import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }

  getMembers(){
    //check if members are filled (API request done), and return members as observable
    if(this.members.length > 0) return of(this.members) ;
    //get<RETURN TYPE> -> Array of Members in this case
    return this.http.get<Member[]>(this.baseUrl + "users").pipe(
      //Map return to members and save it in member Var
      map(members => {
        this.members = members;
        return members;
      })
    );
  }

  getMember(username: string){
    //search for member and save it in constant member
    const member = this.members.find(x => x.username === username);
    //check if a member is found and return member (of = async)
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + "users/" + username);
  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl + "users", member).pipe(
      map(() => {
        //find index of member in array members
        const index = this.members.indexOf(member);
        this.members[index]=member;
      })
    )
  }
}
