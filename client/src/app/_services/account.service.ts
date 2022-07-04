import { HttpClient } from '@angular/common/http';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
//things are stored in the service doesn't get destoried till service closed down
export class AccountService {
  baseUrl = 'https://localhost:5001/api/'
  private currentUserSource = new ReplaySubject<User>(1); //size of the buffer
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User)=>{
        const user = response;
        if(user!= null){
           localStorage.setItem('user',JSON.stringify(user));
           this.currentUserSource.next(user); 
        }
      })
    )
  }

  register(model : any){
    return this.http.post(this.baseUrl + 'account/register',model).pipe(
      map((user: User) => {
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      }) 
    )
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
  }
}