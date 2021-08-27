//Angular Component File.
//Most important building block of Angular.
//Defines which html and css files it uses
//makes http calls and stores it in var which is used by defined html file

import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Dating App';
  users: any;

  //constroctur to define the http request, After construction 
  constructor(private http: HttpClient){}

  //Interface, bc implements OnInit and calls getUsers class
  ngOnInit() {
    this.getUsers();
  }

  getUsers(){
        //http GET request. Subsribe needed so the method does something
        this.http.get("https://localhost:5001/api/users").subscribe(response => {
          //safe response to user Var
          this.users = response;
        }, error => {
          console.log(error);
        });
  }
}
