import { Store, StoreConfig } from '@datorama/akita';
import { Injectable } from '@angular/core';

export interface MessagesState {
    message: HostMessage | null;
    replyMessage: ReplyMessageData[] | null;
}

export interface HostMessage {
    createdDateTime: Date;
    hostUserId: string;
}

export interface ReplyMessageData {
    createdDateTime: Date;
    content: string;
    userId: string;
}

export function createInitialState(): MessagesState {
  return {
    message: null,
    replyMessage: null
  };
}

@StoreConfig({ name: 'message' })
@Injectable({ providedIn: 'root' })
export class MessagesStore extends Store<MessagesState> {
  constructor() {
    super(createInitialState());
  }
}
