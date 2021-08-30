import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastr: ToastrService){}

  canActivate(): Observable<boolean>{
    //get current user
    return this.accountService.currentUser$.pipe(
      map(user => {
        //AuthGuard returns true if user exists
        if (user) return true;
        //If not logged in, send Toastr message
        this.toastr.error("You shall not pass!")
        //return false
        return false;
      })
    )
  }
  
}
