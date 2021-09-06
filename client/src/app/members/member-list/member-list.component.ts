import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgbDropdown } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/User';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
interface genderList {
  value: string;
  display: string;
}

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})




export class MemberListComponent implements OnInit {
  username: String;
  //Observable
 // genderList: Observable<genderList[]>;
  members: Member[];
  pagination: Pagination;
  userParams: UserParams;
  user: User;
  genderList = [{value: 'male', display: 'Males'}, {value: 'female', display: 'females'}];
  disableSelect = new FormControl(false);
  selected: string;


  constructor(private memberService: MembersService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
   
    });
  }

  ngOnInit(): void {
    this.loadMembers();
    //this.genderList
  }

  loadMembers(){
    this.memberService.getMembers(this.userParams).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    });
  }

  resetFilters(){
    //reset User Params
    this.userParams = new UserParams(this.user);
    this.selected = this.userParams.gender;
    this.loadMembers();
  }

  //method called if page is changed (e.g. to page 2)
  pageChanged(event: any){
    this.userParams.pageNumber = event.page;
    this.loadMembers();
  }

  setGender(){
    //this.userParams.gender = gender;
    this.userParams.gender = this.selected;
    this.loadMembers();
    
    //this.loadMembers();
  }

  /*methods to not show own user
  getCurrentUser(): void{
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.username = user.username);
    
    
    
  }
  loadMembers(){
    this.memberService.getMembers().pipe(take(1)).subscribe(members => {
      //Get Members and remove current User from List
      this.members = members.;
    });
  }*/
}
