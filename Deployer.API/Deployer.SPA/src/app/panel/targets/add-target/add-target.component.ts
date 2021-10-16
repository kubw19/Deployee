import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroupExtended } from 'src/app/FormGroupExtended';

import * as tata from 'tata-js'
@Component({
  selector: 'app-add-target',
  templateUrl: './add-target.component.html',
  styleUrls: ['./add-target.component.css']
})
export class AddTargetComponent implements OnInit {

  constructor(private httpClient: HttpClient, private router: Router, private route: ActivatedRoute) { }

  public newTarget = new FormGroupExtended({
    name: new FormControl('', Validators.required),
    hostname: new FormControl('', Validators.required),
    sshport: new FormControl(""),
    sshuser: new FormControl("", Validators.required),
    sshpassword: new FormControl("", Validators.required)
  })

  ngOnInit(): void {
  }
  submit(): void {
    if (!this.newTarget.Validate()) {
      return
    }

    this.httpClient.post("/targets", this.newTarget.value).subscribe(x => {
      tata.success("Success", "Target added successfuly")
      this.router.navigate(["../"], { relativeTo: this.route });
    }, y => {
      tata.error("Error", "Check console for details")
      console.error(y)
    })
  }
}
