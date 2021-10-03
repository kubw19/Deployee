import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
      this.submitDeploy()
    }
  }
  ngOnInit(): void {
    this.httpClient.get("/packages/artifacts").subscribe(x => {
      this.artifacts = x
    })
  }

  deploy(packageName): void {
    this.artifact = packageName
    let options = new ModalOptions();
    options.HeaderText = `Deploy of ${packageName}`
    options.SendText = "Deploy"

    this.httpClient.get(`/packages/artifacts/${packageName}/versions`).subscribe(x => {
      this.httpClient.get("/targets/all").subscribe(y => {
        this.versions = x
        this.targets = y
        console.log(y)
        this.modalInit(options)
      })

    })
  }

  submitDeploy(){
    this.httpClient.post("/deploy", {...this.newDeploy.value, artifact: this.artifact}).subscribe()
  }

}
