import { PersonInfoComponent } from './person-info/person-info.component';
import { PeopleInfoComponent } from './people-info/people-info.component';
import { InfoInputComponent } from './info-input/info-input.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path:'people',component:PeopleInfoComponent},
  {path:'people/:id',component:PersonInfoComponent},
  {path:'addPerson',component:InfoInputComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
