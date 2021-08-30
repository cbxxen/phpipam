import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //Input that comes from homeComponent

  @Output() cancelRegister = new EventEmitter;
  model: any = {};

  //constructore, init AccountService
  constructor(public accountService: AccountService) { }
  
  ngOnInit(): void {
    
  }

  //method to cancel the register function
  cancel(){
    this.cancelRegister.emit(false);
  }

  //method to register User with account.service.ts
  register(){
    this.accountService.register(this.model).subscribe(response =>{
      console.log(response);
      this.cancel();
    })
  }
}
