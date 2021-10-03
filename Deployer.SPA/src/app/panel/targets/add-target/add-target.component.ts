import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-target',
  templateUrl: './add-target.component.html',
  styleUrls: ['./add-target.component.css']
})
export class AddTargetComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }

  private newTarget = new FormGroup({
    name: new FormControl('', Validators.required),
    hostname: new FormControl('', Validators.required),
    sshport: new FormControl(""),
    sshuser: new FormControl(),
    sshpassword: new FormControl()
  })

  ngOnInit(): void {
  }
  submit():void{
    console.log(this.newTarget.value)
    this.httpClient.post("/targets", this.newTarget.value).subscribe()
  }
}
