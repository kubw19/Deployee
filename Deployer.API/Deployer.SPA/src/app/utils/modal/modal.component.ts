import { Component, ContentChild, ElementRef, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  @ViewChild('modalComponent', { static: false }) modal: ElementRef;
  constructor() { }

  @ContentChild(TemplateRef) templateVariable;

  @Output() initHandler = new EventEmitter<Function>();

  @Output() messages = new EventEmitter<string>();

  @Input() action: Function;

  public HeaderText: string
  public SendText: string
  public ModalContent: string

  ngOnInit(): void {
    this.initHandler.emit((
      options: ModalOptions
    ) => { 

      this.HeaderText = options.HeaderText
      this.SendText = options.SendText
      this.ModalContent = options.Content

      let jq: any = $(this.modal.nativeElement)
      jq.modal("show")

     })
  }

  submit(){
    let jq: any = $(this.modal.nativeElement)
    jq.modal("hide")
    this.messages.emit("submit")
  }

}

export class ModalOptions{
  public HeaderText: string
  public SendText: string = "Send"
  public ShowOkButton: boolean = true
  public Content: string = ""
}