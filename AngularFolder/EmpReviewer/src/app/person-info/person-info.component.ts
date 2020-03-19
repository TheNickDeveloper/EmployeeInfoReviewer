import { Person, EmailAddress, Address } from '../models/person';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PeopleService } from '../services/people.service';

@Component({
  selector: 'app-person-info',
  templateUrl: './person-info.component.html',
  styleUrls: ['./person-info.component.css']
})
export class PersonInfoComponent implements OnInit {

  people : Person[] = [];
  newPeople : Person[];
  emailAddresses : EmailAddress[];
  addresses : Address[];
  id: number;

  peopleInfoColumns: any;
  addressColumns: any;
  emailAddressColumns: any;

  constructor(
    private _service: PeopleService,
    private _route: ActivatedRoute) { }

  ngOnInit() {
    this.id = parseInt(this._route.snapshot.paramMap.get('id'));

    this._service.getPeopleInfoById(this.id).subscribe((data)=>{
      console.log(data);
      this.people.push(data);
      
      this.newPeople = this.people;
      this.people.forEach(person => {
        this.addresses = person.addresses;
        this.emailAddresses = person.emailAddresses;
      });
    });

    this.peopleInfoColumns = ['id','firstName','lastName','age'];
    this.addressColumns = ['streetAddress','city','state','zipCode'];
    this.emailAddressColumns = ['emailAddress'];
  }
}
