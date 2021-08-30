import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
  error: any;

  constructor(private router: Router) { 
    //the current Nav is only available one time (when the error happens), goes away after page is reloaded
    const navigation = this.router.getCurrentNavigation();
    //questionmark = if nav is empty no error happens
    this.error = navigation?.extras?.state?.error;
  }

  ngOnInit(): void {
  }

}
