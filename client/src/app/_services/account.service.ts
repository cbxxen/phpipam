import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators'
import { User } from 'src/app/_models/User';



@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  
  //create Observable variable
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  //constructor
  constructor(private http: HttpClient) { }

  //login Method
  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  
  //logout function
  logout(){
    //remove user from Browser Storage
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
  
  //method to register
  register(model: any){
    return this.http.post(this.baseUrl + "account/register", model).pipe(
      map((user: User) => {
        if(user){
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  
  //method to set current User
  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }


}