import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth.service';
import { GraphService } from './graph.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'FrontChatSystem';

  constructor(
    private authService: AuthService,
    private graphService: GraphService
  ){}

  ngOnInit() {
    if (this.authService.account === null) {
      this.authService.login();
    }
    this.getJoinedTeam();
  }

  async getJoinedTeam() {
    const res = await this.graphService.joinedTeam();
    console.log(res);
  }
}
