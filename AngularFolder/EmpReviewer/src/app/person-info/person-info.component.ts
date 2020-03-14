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

  people : Person[];
  emailAddresses : EmailAddress[] = [];
  addresses : Address[] = [];
  id: any;

  constructor(
    private _service: PeopleService,
    private _route: ActivatedRoute) { }

  ngOnInit() {
    this.id = parseInt(this._route.snapshot.paramMap.get('id'));

    this._service.getPeopleInfoById(this.id).subscribe((data)=>{
      console.log(data);
      this.people = data;

      this.people.forEach(person => {
        let emails = person.emailAddresses;
        emails.forEach(email =>{
          this.emailAddresses.push(email);
        })

        let addresses = person.addresses;
        addresses.forEach(address =>{
          this.addresses.push(address);
        })
      });
    });
  }

}
