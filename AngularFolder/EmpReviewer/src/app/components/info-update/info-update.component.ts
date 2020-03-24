import { DialogUpdateComponent } from './../../Dialogs/dialog-update/dialog-update.component';
import { Person, Address, EmailAddress } from './../../models/person';
import { PeopleService } from './../../services/people.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder, FormArray } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-info-update',
  templateUrl: './info-update.component.html',
  styleUrls: ['./info-update.component.css']
})
export class InfoUpdateComponent implements OnInit {

  myForm: FormGroup;
  person: Person;
  id: number;
  addressFormArray: AnalyserNode;

  constructor(
    private fb: FormBuilder,
    private _service: PeopleService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.id = parseInt(this._route.snapshot.paramMap.get('id'));

    this._service.getPeopleInfoById(this.id).subscribe((data) =>{
      this.person = data;

      this.myForm = this.fb.group({
        id: this.id,
        firstName: [this.person.firstName.toString(), Validators.required],
        lastName: [this.person.lastName.toString(), Validators.required],
        age: this.person.age,
        addresses: this.fb.array([]),
        emailAddresses: this.fb.array([])
      });

      this.getAllAddresses(data.addresses);
      this.getAllEmailAddresses(data.emailAddresses);
    });

    this.myForm.valueChanges.subscribe(console.log)
  }

  getAllAddresses(addresses: Address[]){
    addresses.forEach( address => {
      let addressCollection = this.fb.group({
        streetAddress: address.streetAddress,
        city: address.city,
        state: address.state,
        zipCode: address.zipCode
      });

      this.addressForms.push(addressCollection);
    })
  }

  getAllEmailAddresses(emails: EmailAddress[]){
    emails.forEach( email => {
      let emailCollection = this.fb.group({
        emailAddress: [email.emailAddress,Validators.email]
      });

      this.emailForms.push(emailCollection);
    })
  }

  get addressForms(){
    return this.myForm.get('addresses') as FormArray;
  }

  addAddress(){
    const address = this.fb.group({
      streetAddress: [],
      city: [],
      state: [],
      zipCode: []
    });
    this.addressForms.push(address);
  }

  deleteAddress(address : any){
    this.addressForms.removeAt(address);
  }

  get emailForms(){
    return this.myForm.get('emailAddresses') as FormArray;
  }
  
  addEmailAddress(){
    const emailAddress = this.fb.group({
      emailAddress: ['',Validators.email]
    });
    this.emailForms.push(emailAddress);
  }

  deleteEmailAddress(email : any){
    this.emailForms.removeAt(email);
  }

  putDataToDb(){
    let formData = this.myForm.getRawValue();
    let serializedFormData = JSON.stringify(formData);

    console.log(serializedFormData);

    this._service.putPersonInfo(this.id, serializedFormData).subscribe(data => {
      console.log(data);
    });

    let result = this._dialog.open(DialogUpdateComponent);
    result.afterClosed().subscribe(r =>{
      this._router.navigate(['/people']);
    });
  }
}
