import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { PeopleService } from '../services/people.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogSummitComponent } from '../dialog-summit/dialog-summit.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-info-input',
  templateUrl: './info-input.component.html',
  styleUrls: ['./info-input.component.css']
})
export class InfoInputComponent implements OnInit {

  myForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _service: PeopleService,
    private _dialog: MatDialog,
    private _router:Router) { }

  ngOnInit(): void {

    this.myForm = this.fb.group({
      firstName: ['',Validators.required],
      lastName: ['',Validators.required],
      age: '',
      addresses: this.fb.array([]),
      emailAddresses: this.fb.array([])
    })

    this.myForm.valueChanges.subscribe(console.log)
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

  postDataToDb(){
    let formData = this.myForm.getRawValue();
    let serializedFormData = JSON.stringify(formData);
    this._service.postPersonInfo(serializedFormData).subscribe(data => {
      console.log(data);
    });

    let result = this._dialog.open(DialogSummitComponent);
    result.afterClosed().subscribe(r =>{
      this._router.navigate(['/people']);
    });
  }
}
