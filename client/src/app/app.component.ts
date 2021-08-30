//Angular Component File.
//Most important building block of Angular.
//Defines which html and css files it uses
//makes http calls and stores it in var which is used by defined html file

import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { User } from './_models/User';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Dating App';
  users: any;

  //constroctur to define the http request, After construction 
  constructor(private AccountService: AccountService){}

  //Interface, bc implements OnInit and calls getUsers class
  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser(){
    //get User Object from browser and parse it to User Object
    const user: User = JSON.parse(localStorage.getItem('user')!);
    this.AccountService.setCurrentUser(user);

  }
}
