import { HomePageComponent } from './components/home-page/home-page.component';
import { InfoUpdateComponent } from './components/info-update/info-update.component';
import { PersonInfoComponent } from './components/person-info/person-info.component';
import { PeopleInfoComponent } from './components/people-info/people-info.component';
import { InfoInputComponent } from './components/info-input/info-input.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path:'',component:HomePageComponent},
  {path:'people',component:PeopleInfoComponent},
  {path:'people/:id',component:PersonInfoComponent},
  {path:'addPerson',component:InfoInputComponent},
  {path:'updatePerson/:id',component:InfoUpdateComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
