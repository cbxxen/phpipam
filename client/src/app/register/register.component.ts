import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, ControlContainer, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
  registerForm: FormGroup;
  maxDate: Date;
  //used to show errors during registration
  validationErrors: string[] = [];

  //constructore, init AccountService
  constructor(public accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) { }
  
  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date();
    //only allow people older than 18
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm(){
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, 
        Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValue('password')]]
    })
    //update validity of confirmPassword as soon as a change is made in password field
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValue(matchTo: string): ValidatorFn{
    //create control Object which is checked for match
    return (control:AbstractControl) =>{
      //if return of control is null the method return isMatching=true
      //controll = Form Obj that calls functoin (ConfirmPassword)
      return control?.value === control?.parent?.controls[matchTo].value ? null : {isMatching: true}
    }
  }

  //method to cancel the register function
  cancel(){
    this.cancelRegister.emit(false);
  }

  //method to register User with account.service.ts
  register(){
    this.accountService.register(this.registerForm.value).subscribe(response =>{
      this.toastr.success("Welcome onboard!");
      //navigate to /member page
      this.router.navigateByUrl('/member');
    }, error => {
      this.validationErrors = error;
    })
  }
}
