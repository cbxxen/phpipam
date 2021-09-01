import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  username: String;
  //Observable
  members$: Observable<Member[]>;

  constructor(private memberService: MembersService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.members$ = this.memberService.getMembers();
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
