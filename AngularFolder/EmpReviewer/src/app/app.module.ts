import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PeopleInfoComponent } from './components/people-info/people-info.component';
import { PersonInfoComponent } from './components/person-info/person-info.component';
import { PeopleService } from './services/people.service';

import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule} from '@angular/forms';

import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule} from '@angular/material/toolbar';
import { MatDialogModule} from '@angular/material/dialog';
import { MatIconModule} from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

import { InfoInputComponent } from './components/info-input/info-input.component';
import { DialogSummitComponent } from './Dialogs/dialog-summit/dialog-summit.component';
import { DialogDeleteComponent } from './Dialogs/dialog-delete/dialog-delete.component';
import { InfoUpdateComponent } from './components/info-update/info-update.component';
import { DialogUpdateComponent } from './Dialogs/dialog-update/dialog-update.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { FlexLayoutModule } from '@angular/flex-layout';


@NgModule({
  declarations: [
    AppComponent,
    PeopleInfoComponent,
    PersonInfoComponent,
    InfoInputComponent,
    DialogSummitComponent,
    DialogDeleteComponent,
    InfoUpdateComponent,
    DialogUpdateComponent,
    HomePageComponent,
  ],
  entryComponents:[
    DialogSummitComponent,
    DialogDeleteComponent,
    DialogUpdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCheckboxModule,
    MatChipsModule,
    MatFormFieldModule,
    MatTableModule,
    MatToolbarModule,
    MatDialogModule,
    MatIconModule,
    MatPaginatorModule,
    MatSortModule,
    FlexLayoutModule
  ],

  // dependency inject
  providers: [
    PeopleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
