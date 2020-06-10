import { MessagesStore } from './messages.store';
import { Injectable } from '@angular/core';
import { WebApiService } from 'src/app/services/webapi.service';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { SignalRConnectionInfo } from 'src/app/services/signalr-connection-info.model';

@Injectable({
    providedIn: 'root'
})
export class MessagesService {
  private hubConnection: HubConnection | undefined;
  private messageId = '';

  constructor(
    private messageStore: MessagesStore,
    private webApi: WebApiService
  ) {}

  setSignalRHub() {
    this.webApi.startSubscription();
    this.webApi.getConnectionInfo().subscribe(info => {
      this.hubConncetionSettings(info);
    });
  }

  private hubConncetionSettings(info: SignalRConnectionInfo) {
    console.log('SETTING CONNECTION');
    console.log(info);
    const options = {
      accessTokenFactory: () => info.accessToken
    };

    this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(info.url, options)
        .configureLogging(signalR.LogLevel.Information)
        .build();

    this.hubConnection.start().catch(err => console.error(err.toString()));
    this.hubConnection.on('notify', (data: any) => {
      console.log({NOTIFY: data});
      this.getMessages(this.messageId);
    });
  }

  getMessages(messageId: string) {
    this.messageId = messageId;
    this.webApi.getMessage(messageId).subscribe(x => {
      this.messageStore.update({
        message: x.message,
        replyMessage: x.replyMessage
      });
    });
  }

  replyMessage(message: string) {
    this.webApi.replyMessage(this.messageId, message).subscribe(() => console.log('reply'));
  }

  destory() {
    this.messageStore.destroy();
  }
}
