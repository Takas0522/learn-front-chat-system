import { Injectable } from '@angular/core';
import * as graph from '@microsoft/microsoft-graph-client';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class GraphService {

  private client: graph.Client;
  constructor(
    private authService: AuthService
  ) {
    this.client = graph.Client.init({
      authProvider: async (done) => {
        const token = await this.authService.acquireTokenSilent(['user.read.all']);
        if (token) {
          done(null, token);
        } else {
          done('Cant not Get Token', null);
        }
      }
    });
  }

  async joinedTeam() {
    this.client.api('/me/joinedTeams').get();
  }
}
