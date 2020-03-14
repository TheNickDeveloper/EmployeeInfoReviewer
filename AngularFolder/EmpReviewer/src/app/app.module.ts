import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpModule} from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PeopleInfoComponent } from './people-info/people-info.component';
import { HttpClientModule } from '@angular/common/http';
import { PersonInfoComponent } from './person-info/person-info.component';
import { PeopleService } from './services/people.service';

@NgModule({
  declarations: [
    AppComponent,
    PeopleInfoComponent,
    PersonInfoComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],

  // dependency inject
  providers: [
    PeopleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
