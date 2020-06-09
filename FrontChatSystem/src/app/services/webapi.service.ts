import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessagesState } from '../components/chat-window/chat/messages/messages.store';
import { SignalRConnectionInfo } from './signalr-connection-info.model';


@Injectable({
  providedIn: 'root'
})
export class WebApiService {

  private readonly endpointUrl = '/api';
  private readonly LOCALSTORAGEKEY = 'MESSAGE_ID';

  constructor(
    private http: HttpClient
  ) {}

  createMessage(message: string): Observable<string> {
    const formData = new FormData();
    formData.append('message', message);
    return this.http.post<string>(`${this.endpointUrl}/message`, formData);
  }

  getMessage(messageId: string): Observable<MessagesState> {
    const param = new HttpParams().append('messageId', messageId);
    return this.http.get<MessagesState>(`${this.endpointUrl}/message`, { params: param });
  }

  replyMessage(messageId: string, message: string): Observable<void> {
    const formData = new FormData();
    formData.append('messageId', messageId);
    formData.append('message', message);
    return this.http.put<void>(`${this.endpointUrl}/message`, formData);
  }

  startSubscription() {
    return this.http.request('POST', `${this.endpointUrl}/Subscription`).subscribe(() => console.log('Set Subscription'));
  }
  getConnectionInfo() {
    const requestUrl = `https://okawa-sample-subscription.azurewebsites.net/api/Negotiate?code=KALBdHhpxaRlSLSOVazFAlAFS56WuiVLUw1iqP1y/1/Vc8/hMdwWUA==`;
    return this.http.get<SignalRConnectionInfo>(requestUrl);
  }
  subscriptionNegtiation() {
    this.getConnectionInfo().subscribe(info => {

    });
  }
}
