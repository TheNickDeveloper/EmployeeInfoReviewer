import { DialogDeleteComponent } from './../dialog-delete/dialog-delete.component';
import { Router } from '@angular/router';
import { PeopleService } from '../services/people.service';
import { Component, OnInit } from '@angular/core';
import { Person } from '../models/person';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';

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
    private _dialog: MatDialog,
    private _router: Router) { }

  ngOnInit() {
    this._service.getPeopleInfo().subscribe((data)=>{
      console.log(data);
      this.people = data;
    });
    this.displayedColumns = ['id','firstName','lastName','age','detail','delete'];
  }

  navigateToDetailPage(id:number){
    this._router.navigate(['/people/' + id]);
  }

  deletePerson(id:number){
    let dialogConfig = new MatDialogConfig();
    let isDeleteRecord: boolean;

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
        
    let result = this._dialog.open(DialogDeleteComponent, dialogConfig);

    result.afterClosed().subscribe(r =>{
      isDeleteRecord = r;

      if(isDeleteRecord){
        this._service.deletePersonInfo(id).subscribe(() => {
          this.fetchData();
        });
      }
      else{
        console.log("cancle");
      }
    });
  }

  fetchData() {
    this._service.getPeopleInfo().subscribe(data =>{
      this.people = data;
    });
  }

}
