import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';


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
  constructor(public accountService: AccountService, private toastr: ToastrService) { }
  
  ngOnInit(): void {
    
  }

  //method to cancel the register function
  cancel(){
    this.cancelRegister.emit(false);
  }

  //method to register User with account.service.ts
  register(){
    this.accountService.register(this.model).subscribe(response =>{
      this.toastr.success("Welcome onboard!");
      this.cancel();
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    })
  }
}
