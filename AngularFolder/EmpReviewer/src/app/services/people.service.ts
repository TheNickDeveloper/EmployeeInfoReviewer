import { Person } from './../models/person';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  public baseUrl:string =environment.ApiBaseUrl;
  public httpOptions ={
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'my-auth-token'
    })
  };

  constructor(private httpClient: HttpClient){
  }

  getPeopleInfo(): Observable<Person[]>{
    let peopleRul = this.baseUrl + 'api/people';
    return this.httpClient.get<Person[]>(peopleRul);
  }

  getPeopleInfoById(id : number ): Observable<Person>{
    let peopleRul = this.baseUrl + 'api/people/' + id;
    console.log(id);
    console.log(peopleRul);
    return this.httpClient.get<Person>(peopleRul);
  }

  postPersonInfo(inputPerson: any){
    console.log(inputPerson);
    let peopleRul = this.baseUrl + 'api/people';
    return this.httpClient.post(peopleRul, inputPerson, this.httpOptions);
  }

  putPersonInfo(id: number, inputPerson: any){
    console.log(inputPerson);
    let peopleRul = this.baseUrl + 'api/people/'+ id;
    return this.httpClient.put(peopleRul, inputPerson, this.httpOptions);
  }

  deletePersonInfo(id: number):Observable<void>{
    let peopleRul = this.baseUrl + 'api/people/' + id;
    return this.httpClient.delete<void>(peopleRul);
  }
}
