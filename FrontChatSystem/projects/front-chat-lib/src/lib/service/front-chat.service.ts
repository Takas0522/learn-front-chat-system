import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IMessageWithReply } from '../components/chat-window/chat/models/message-with-reply';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

interface ISignalRConnectionInfo {
  url: string;
  accessToken: string;
}

@Injectable({
  providedIn: 'root'
})
export class FrontChatService {

  private readonly endpointUrl = '/api';
  private readonly LOCALSTORAGEKEY = 'MESSAGE_ID';
  private messageIdp = '';
  get messageId() {
    return this.messageIdp;
  }
  private hubConnection: HubConnection | undefined;

  constructor(
    private http: HttpClient
  ) {
    this.messageIdp = this.loadMessageId();
  }

  createMessage(message: string): Observable<string> {
    const formData = new FormData();
    formData.append('message', message);
    return this.http.post<string>(`${this.endpointUrl}/message`, formData);
  }

  getMessage(messageId: string): Observable<IMessageWithReply> {
    const param = new HttpParams().append('messageId', messageId);
    return this.http.get<IMessageWithReply>(`${this.endpointUrl}/message`, { params: param });
  }

  replyMessage(messageId: string, message: string): Observable<void> {
    const formData = new FormData();
    formData.append('messageId', messageId);
    formData.append('message', message);
    return this.http.put<void>(`${this.endpointUrl}/message`, formData);
  }

  saveMessageId(messageId: string) {
    localStorage.setItem(this.LOCALSTORAGEKEY, messageId);
  }

  loadMessageId(): string {
    const load = localStorage.getItem(this.LOCALSTORAGEKEY);
    if (load) {
      return load;
    }
    return '';
  }

  subscriptionNegtiation() {
    this.getConnectionInfo().subscribe(info => {
      const options = {
          accessTokenFactory: () => info.accessToken
      };

      this.hubConnection = new signalR.HubConnectionBuilder()
          .withUrl(info.url, options)
          .configureLogging(signalR.LogLevel.Information)
          .build();

      this.hubConnection.start().catch(err => console.error(err.toString()));

      this.hubConnection.on('notify', (data: any) => {
          console.log(data);
      });
    });
  }

  private getConnectionInfo() {
    const requestUrl = `https://okawa-sample-subscription.azurewebsites.net/api/Negotiate?code=KALBdHhpxaRlSLSOVazFAlAFS56WuiVLUw1iqP1y/1/Vc8/hMdwWUA==`;
    return this.http.get<ISignalRConnectionInfo>(requestUrl);
  }

  startSubscription() {
    return this.http.request('POST', `${this.endpointUrl}/Subscription`).subscribe(() => console.log('Set Subscription'));
  }

  exitSession() {
    localStorage.removeItem(this.LOCALSTORAGEKEY);
  }
}
