import { Router } from '@angular/router';
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
  displayedColumns: any;

  constructor(
    private _service: PeopleService,
    private _router: Router) { }

  ngOnInit() {
    this._service.getPeopleInfo().subscribe((data)=>{
      console.log(data);
      this.people = data;
    });
    this.displayedColumns = ['id','firstName','lastName','age','detail','delete'];
  }

  deletePerson(id:number){
    this._service.deletePersonInfo(id).subscribe(()=> 
    {
      this.fetchData();
    });
  }

  navigateToDetailPage(id:number){
    this._router.navigate(['/people/' + id]);
  }

  fetchData() {
    this._service.getPeopleInfo().subscribe(data =>{
      this.people = data;
    });
  }
}
