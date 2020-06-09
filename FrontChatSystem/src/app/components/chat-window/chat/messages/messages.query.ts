import { Query } from '@datorama/akita';
import { MessagesStore, MessagesState } from './messages.store';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class MessagesQuery extends Query<MessagesState> {
  messagesState$ = this.select();
  constructor(protected store: MessagesStore) {
    super(store);
  }
}
