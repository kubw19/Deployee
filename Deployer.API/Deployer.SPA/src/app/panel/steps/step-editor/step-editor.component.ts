import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'step-editor',
  templateUrl: './step-editor.component.html',
  styleUrls: ['./step-editor.component.css']
})
export class StepEditorComponent implements OnInit {

  settingsForm: FormGroup;
  properties: FormArray;

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient) {
  }
  @Input() step!: any;
  @Output() onDelete = new EventEmitter<any>();
  @Output() onNewAdded = new EventEmitter<any>();

  public isEditMode: boolean

  ngOnInit(): void {
    this.isEditMode = this.step.id == 0
    let props = {}
    for (let x of this.step.inputProperties) {
      props[x.name] = x.value ?? x.defaultValue
    }

    let settings = {
      name: this.step.name,
      properties: this.formBuilder.group(props)
    }

    this.settingsForm = this.formBuilder.group(settings)
  }

  delete() {
    this.onDelete.emit(this.step)
    if (this.step.id != 0) {
      this.httpClient.delete(`projects/deletestep?stepId=${this.step.id}`).subscribe()
    }
  }

  cancel() {
    if (this.step.id == 0) {
      this.delete()
    } else {
      this.isEditMode = false;
    }
  }

  edit() {
    this.isEditMode = true
  }

  save() {

    for (let x of this.step.inputProperties) {
      x.value = this.settingsForm.value.properties[x.name]
    }
    console.log(this.settingsForm.value)
    console.log(this.step)
    let updateModel = {
      Name: this.settingsForm.value.name,
      InputProperties: this.step.inputProperties,
      ProjectId: 1,
      Type: this.step.type,
      Id: this.step.id
    }
    this.httpClient.post("/projects/NewStep", updateModel).subscribe((x: any) => {

      this.httpClient.get(`/projects/steps/${this.step.id != 0 ? this.step.id : x.id}`).subscribe(y => {
        this.step = y
      })

    })

    this.isEditMode = false

    console.log(updateModel)
  }

}
