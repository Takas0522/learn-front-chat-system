import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChatWindowService } from './chat-window.service';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.scss']
})
export class ChatWindowComponent implements OnInit {

  constructor(
    private router: Router,
    private service: ChatWindowService
  ) { }

  ngOnInit(): void {
    const messageId = this.service.loadMessageId();
    if (messageId) {
      this.router.navigate(['chat', messageId]);
    } else  {
      this.router.navigate(['connection']);
    }
  }

}
