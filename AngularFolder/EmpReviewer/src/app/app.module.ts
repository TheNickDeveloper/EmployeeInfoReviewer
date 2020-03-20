import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PeopleInfoComponent } from './people-info/people-info.component';
import { PersonInfoComponent } from './person-info/person-info.component';
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

import { InfoInputComponent } from './info-input/info-input.component';
import { DialogSummitComponent } from './dialog-summit/dialog-summit.component';

@NgModule({
  declarations: [
    AppComponent,
    PeopleInfoComponent,
    PersonInfoComponent,
    InfoInputComponent,
    DialogSummitComponent,
  ],
  entryComponents:[
    DialogSummitComponent
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
    MatIconModule
  ],

  // dependency inject
  providers: [
    PeopleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
