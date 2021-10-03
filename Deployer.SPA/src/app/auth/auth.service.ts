import { Injectable } from '@angular/core';

import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  private manager = new UserManager(getClientSettings());
  private user: User | null;

  constructor() { }

  login() {
    return this.manager.signinRedirect({ state: window.location.href });
  }

  logout() {
    return this.manager.signoutRedirect();
  }

  async completeAuth() {
    return this.manager.signinRedirectCallback();
  }

  getUser(): User | null {
    return this.user
  }

  async isAuthenticated(): Promise<boolean> {
    this.user = await this.manager.getUser()
    if (!this.user) {
      return false;
    }
    return !this.user.expired;
  }

  get authorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }


}
export function getClientSettings(): UserManagerSettings {
  return {
    authority:  environment.authority,
    client_id: 'worktimemanager.spa',
    redirect_uri: environment.host + "/auth-callback",
    post_logout_redirect_uri: environment.host + "/bye",
    response_type: "id_token token",
    scope: "openid profile worktimemanager.api.basic",
    filterProtocolClaims: true,
  };
}