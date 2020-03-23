import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
  styleUrls: ['./dialog-delete.component.css']
})
export class DialogDeleteComponent implements OnInit {

  constructor( private dialogRef: MatDialogRef<DialogDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) data) { }
  
  isDeleteRecord: boolean;

  ngOnInit(): void {
    this.isDeleteRecord = false;
  }

  delete(){
    this.isDeleteRecord = true;
    this.dialogRef.close(this.isDeleteRecord);
  }

  cancle(){
    this.dialogRef.close(this.isDeleteRecord);
  }

}
