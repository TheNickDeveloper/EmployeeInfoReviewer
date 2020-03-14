import { Observable } from 'rxjs/internal/Observable';
import { Person } from '../models/person';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  public people: Person[];
  public baseUrl:string =environment.ApiBaseUrl; 

  constructor(private httpClient: HttpClient){
  }

  getPeopleInfo(): Observable<any>{
    let peopleRul = this.baseUrl + 'api/people';
    return this.httpClient.get(peopleRul);
  }

  getPeopleInfoById(id : number ): Observable<any>{
    let peopleRul = this.baseUrl + 'api/people/' + id;
    return this.httpClient.get(peopleRul);
  }
}
