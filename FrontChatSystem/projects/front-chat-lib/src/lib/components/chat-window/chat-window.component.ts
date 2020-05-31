import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FrontChatService } from '../../service/front-chat.service';

@Component({
  selector: 'fcl-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.scss']
})
export class ChatWindowComponent implements OnInit {

  constructor(
    private router: Router,
    private service: FrontChatService
  ) { }

  ngOnInit(): void {
    if (this.service.messageId !== '') {
      this.router.navigate(['chat', this.service.messageId]);
    } else  {
      this.router.navigate(['connection']);
    }
  }

}
