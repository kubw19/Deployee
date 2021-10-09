import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {



  constructor() { }

  login() {

  }

  logout() {

  }


}




  



export function getClientSettings() {
  return {
    authority: environment.authority,
    client_id: 'worktimemanager.spa',
    redirect_uri: environment.host + "/auth-callback",
    post_logout_redirect_uri: environment.host + "/bye",
    response_type: "id_token token",
    scope: "openid profile worktimemanager.api.basic",
    filterProtocolClaims: true,
  };
}