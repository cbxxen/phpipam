import { User } from "./User";

//user Params for member.sevice.ts
export class UserParams {
    gender: string;
    minAge = 18;
    maxAge = 99;
    pageNumber = 1;
    pageSize = 5;

    constructor(user: User){
        //set gender to opposite of user Gender
        this.gender = user.gender ==='female' ? 'male' : 'female';
    }
}