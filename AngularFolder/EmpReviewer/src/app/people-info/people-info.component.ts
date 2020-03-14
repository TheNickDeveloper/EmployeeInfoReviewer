import { PeopleService } from '../services/people.service';
import { Component, OnInit } from '@angular/core';
import { Person } from '../models/person';

@Component({
  selector: 'app-people-info',
  templateUrl: './people-info.component.html',
  styleUrls: ['./people-info.component.css']
})

export class PeopleInfoComponent implements OnInit {

  people : Person[];

  constructor(private _service: PeopleService) { }

  ngOnInit() {
    this._service.getPeopleInfo().subscribe((data)=>{
      console.log(data);
      this.people = data;
    });
  }
}
