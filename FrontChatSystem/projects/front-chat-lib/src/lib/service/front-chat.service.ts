import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IMessageWithReply } from '../components/chat-window/chat/models/message-with-reply';

@Injectable({
  providedIn: 'root'
})
export class FrontChatService {

  private readonly endpointUrl = '/api';
  constructor(
    private http: HttpClient
  ) { }

  createMessage(message: string): Observable<string> {
    const formData = new FormData();
    formData.append('message', message);
    return this.http.post<string>(`${this.endpointUrl}/message`, formData);
  }

  getMessage(messageId: string): Observable<IMessageWithReply> {
    const param = new HttpParams().append('messageId', messageId);
    return this.http.get<IMessageWithReply>(`${this.endpointUrl}/message`, { params: param });
  }
}
