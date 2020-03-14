import { PersonInfoComponent } from './person-info/person-info.component';
import { PeopleInfoComponent } from './people-info/people-info.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path:'people',component:PeopleInfoComponent},
  {path:'people/:id',component:PersonInfoComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
