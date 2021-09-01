import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  //count to check how many request ongoing rn
  busyRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) {}

  //while API request is ongoing
  busy(){
    this.busyRequestCount++;
    //show spinner with it options
    this.spinnerService.show(undefined,{
      type:'line-scale-party',
      bdColor: "rgba(255,255,255,0)",
      color:"#333333"
    });
  }

  //finished API request
  idle(){
    this.busyRequestCount--;
    //set busyRequestCount to 0 if it is lower 
    if(this.busyRequestCount <= 0){
      this.busyRequestCount = 0;
      //hide spinner again
      this.spinnerService.hide();

    }
  }
}
