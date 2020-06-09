import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatWindowService {
  private readonly LOCALSTORAGEKEY = 'MESSAGE_ID';
  constructor() { }

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
}
