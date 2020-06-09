import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable, of } from 'rxjs';
import { switchMap, mergeMap } from 'rxjs/operators';
import { MessagesState } from './messages/messages.store';
import { MessagesQuery } from './messages/messages.query';
import { MessagesService } from './messages/messages.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {

  messages: MessagesState | null = null;
  inputValue = '';

  constructor(
    private route: ActivatedRoute,
    private query: MessagesQuery,
    private messagesService: MessagesService
  ) { }

  ngOnInit(): void {
    this.query.messagesState$.subscribe(x => {
      this.messages = x;
    });
    this.routerSetting();
  }

  private routerSetting() {
    this.route.paramMap.subscribe(x => {
      const id = x.get('id') ? x.get('id') : '';
      if (id) {
        this.messagesService.getMessages(id);
        this.messagesService.setSignalRHub();
      }
    });
  }

  replyMessage() {
    this.messagesService.replyMessage(this.inputValue);
    this.inputValue = '';
  }

  ngOnDestroy() {
    this.messagesService.destory();
  }

}
