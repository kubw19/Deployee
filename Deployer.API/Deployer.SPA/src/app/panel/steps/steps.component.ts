import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import interact from 'interactjs'
import { NestableSettings } from 'ngx-nestable/lib/nestable.models';
import { StepEditorComponent } from './step-editor/step-editor.component';

@Component({
  selector: 'app-steps',
  templateUrl: './steps.component.html',
  styleUrls: ['./steps.component.css']
})
export class StepsComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }

  public stepTypes;


  public options = {
    fixedDepth: true
  } as NestableSettings;
  public list = [

  ];

  add(typeId): void {
    this.httpClient.get(`/projects/steptemplates/${typeId}`).subscribe((x: any) => {
      this.list.push(x)
    })
  }

  ngOnInit(): void {
    this.httpClient.get("projects/steptemplates").subscribe(x => this.stepTypes = x)
    this.httpClient.get(`projects/${1}/steps`).subscribe(x => this.addExisting(x))
  }

  addExisting(items) {
    for (let item of items) {
      this.list.push(item)
    }
  }

  removeStep(step) {
    this.list = this.list.filter(x => x !== step)
  }

}
