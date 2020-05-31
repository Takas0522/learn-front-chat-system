import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { FrontChatService } from '../../../service/front-chat.service';
import { Observable, of } from 'rxjs';
import { IMessageWithReply, MessageWithReply } from './models/message-with-reply';

@Component({
  selector: 'fcl-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  private message$!: Observable<IMessageWithReply | null>;
  message: MessageWithReply | null = null;

  constructor(
    private route: ActivatedRoute,
    private service: FrontChatService
  ) { }

  ngOnInit(): void {
    this.message$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        const id = params.get('id') ? params.get('id') : '';
        if (id) {
          return this.service.getMessage(id);
        }
        return of(null);
      })
    );
    this.message$.subscribe(x => {
      if (x) {
        this.message = new MessageWithReply(x);
      } else {
        this.message = null;
      }
      console.log(this.message)
    });
  }

}
