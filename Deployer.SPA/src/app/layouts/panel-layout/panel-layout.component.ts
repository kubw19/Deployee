import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-panel-layout',
  templateUrl: './panel-layout.component.html',
  styleUrls: ['./panel-layout.component.scss']
})
export class PanelLayoutComponent implements OnInit {

  showNotifications = false

  constructor() { }

  public profileImageURL: string
  public currentUser
  public username


  public companyId


  async ngOnInit() {
    this.update()
    setInterval(x => this.update(), 5000)
  }
  async update() {

  }

  signout() {

  }

}
