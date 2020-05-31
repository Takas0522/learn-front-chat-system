import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap, mergeMap } from 'rxjs/operators';
import { FrontChatService } from '../../../service/front-chat.service';
import { Observable, of, interval } from 'rxjs';
import { IMessageWithReply, MessageWithReply, IReplyMessageData } from './models/message-with-reply';

@Component({
  selector: 'fcl-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  message: MessageWithReply | null = null;
  hostId = '';
  inputValue = '';
  private messageId = '';

  constructor(
    private route: ActivatedRoute,
    private service: FrontChatService
  ) { }

  ngOnInit(): void {
    const message$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        const id = params.get('id') ? params.get('id') : '';
        if (id) {
          this.messageId = id;
          return this.service.getMessage(id);
        }
        return of(null);
      })
    );
    message$.subscribe(x => {
      if (x) {
        this.getMessage$(this.messageId);
      }
    });

  }

  private getMessage$(id: string) {
    interval(5000).pipe(
      mergeMap(() => {
        return this.service.getMessage(id);
      })
    ).subscribe(x => {
      this.setMessage(x);
    });
  }

  private setMessage(message: IMessageWithReply | null) {
    if (message) {
      this.message = new MessageWithReply(message);
      this.hostId = this.message.message.hostUserId;
    } else {
      this.message = null;
    }
    console.log(message);
  }

  replyMessage() {
    this.service.replyMessage(this.messageId, this.inputValue).subscribe(() => {
      this.service.getMessage(this.messageId).subscribe(x => {
        this.setMessage(x);
      });
    });
  }

}
