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
    this.update()
  }

  update() {
    this.httpClient.get("/targets").subscribe(w => {
      this.targets = w
    })
  }

  remove(id) {
    this.httpClient.delete(`/targets/${id}`).subscribe(x => this.update())
  }
}
