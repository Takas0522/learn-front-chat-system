import { Injectable } from '@angular/core';
import { UserAgentApplication, Account } from 'msal';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private msalClient: UserAgentApplication;
  account: Account | null = null;

  constructor() {
    this.msalClient = new UserAgentApplication(environment.msalConfig);
    this.msalClient.handleRedirectCallback((authErr, response) => {
      if (authErr) {
        console.log({ authErr });
      } else {
        this.account = response.account;
      }
    });
  }

  async login() {
    const res = await this.msalClient.loginPopup();
    this.account = res.account;
    console.log(this.account)
  }

  async acquireTokenSilent(scopes: string[]) {
    const res = await this.msalClient.acquireTokenSilent({ scopes });
    return res.accessToken;
  }
}
