import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list-targets',
  templateUrl: './list-targets.component.html',
  styleUrls: ['./list-targets.component.css']
})
export class ListTargetsComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }

  public targets

  ngOnInit(): void {
    this.httpClient.get("/targets").subscribe(w=>{
      this.targets = w
      console.log(this.targets)
    })
  }

}
