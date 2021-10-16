import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Helpers } from 'src/app/Helpers';
import { ModalOptions } from 'src/app/utils/modal/modal.component';

@Component({
  selector: 'app-list-artifacts',
  templateUrl: './list-artifacts.component.html',
  styleUrls: ['./list-artifacts.component.css']
})
export class ListArtifactsComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }

  public artifacts
  public versions
  public targets
  public artifact


  public newDeploy = new FormGroup({
    version: new FormControl('', Validators.required),
    targetId: new FormControl('', Validators.required),
  })

  public modalInit: Function
  receiveModalMessage(message) {
    if (message == "submit") {
      
    }
  }
  ngOnInit(): void {
    this.httpClient.get(`/artifacts/projects/${1}`).subscribe((x: Array<any>) => {
      this.artifacts = x.map(x => { return { ...x, channels: Helpers.GroupBy(x.versions, "channelId") } })
    })
  }





}
