import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControlOptions, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormGroupExtended } from 'src/app/FormGroupExtended';

@Component({
  selector: 'step-editor',
  templateUrl: './step-editor.component.html',
  styleUrls: ['./step-editor.component.css']
})
export class StepEditorComponent implements OnInit {

  settingsForm: FormGroup
    ;
  properties: FormArray;

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient) {
  }
  @Input() step!: any;
  @Output() onDelete = new EventEmitter<any>();
  @Output() onNewAdded = new EventEmitter<any>();


  public artifacts
  public targetRoles

  public isEditMode: boolean

  public validator = FormGroupExtended.IsNotValidStatic

  ngOnInit(): void {
    this.isEditMode = this.step.id == 0
    this.setUpForm()

    this.httpClient.get(`/artifacts/projects/${1}`).subscribe(x => {
      this.artifacts = x
    })

    this.httpClient.get(`/targets/roles`).subscribe(x => {
      this.targetRoles = x
    })

  }

  private setUpForm() {
    let props = {}
    for (let x of this.step.inputProperties) {
      props[x.name] = (x.type=="Int32" && x.value == 0) ? null : x.value
    }

    let opts: AbstractControlOptions = {
      validators: Validators.required
    }

    let settings = {
      name: this.step.name,
      properties: this.formBuilder.group(props, opts)
    }


    this.settingsForm = this.formBuilder.group(settings)

  }

  delete() {
    if (this.step.id != 0) {
      this.httpClient.delete(`projects/step/${this.step.id}`).subscribe(x => {
        this.onDelete.emit(this.step)
      })
    } else {
      this.onDelete.emit(this.step)
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

    if (!FormGroupExtended.ValidateStatic(this.settingsForm)) {
      return
    }

    for (let x of this.step.inputProperties) {
      x.value = this.settingsForm.value.properties[x.name]
    }

    let updateModel = {
      Name: this.settingsForm.value.name,
      InputProperties: this.step.inputProperties,
      ProjectId: 1,
      Type: this.step.type,
      Id: this.step.id
    }
    this.httpClient.post("/projects/steps", updateModel).subscribe((x: any) => {
      this.httpClient.get(`/projects/steps/${this.step.id != 0 ? this.step.id : x.value}`).subscribe(y => {
        this.step = y["value"]
      })

    })

    this.isEditMode = false
  }

}
