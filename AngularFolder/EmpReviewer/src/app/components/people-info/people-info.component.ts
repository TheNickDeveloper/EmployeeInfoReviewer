import { DialogDeleteComponent } from '../../Dialogs/dialog-delete/dialog-delete.component';
import { Router } from '@angular/router';
import { PeopleService } from '../../services/people.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Person } from '../../models/person';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-people-info',
  templateUrl: './people-info.component.html',
  styleUrls: ['./people-info.component.css']
})

export class PeopleInfoComponent implements OnInit, AfterViewInit {

  people : Person[];
  displayedColumns: any;
  public dataSource = new MatTableDataSource<Person>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private _service: PeopleService,
    private _dialog: MatDialog,
    private _router: Router) { }


  ngOnInit() {
    this._service.getPeopleInfo().subscribe((data)=>{
      console.log(data);
      this.dataSource.data = data as Person[];
    });
    this.displayedColumns = ['id','firstName','lastName','age','detail','update','delete'];
  }

  ngAfterViewInit():void{
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  navigateToDetailPage(id:number){
    this._router.navigate(['/people/' + id]);
  }

  navigateToPersonUpdatePage(id:number){
    this._router.navigate(['/updatePerson/' + id]);
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
      this.dataSource.data = data as Person[];
    });
  }

  doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

}
