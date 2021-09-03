import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements ControlValueAccessor {
  //input property value
  @Input() label: string;
  //get the type of input property
  @Input() type = 'text';

  //self = everything will be injected to this controller
  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
   }

  writeValue(obj: any): void {
  }
  
  registerOnChange(fn: any): void {
  }
  
  registerOnTouched(fn: any): void {
  }
}
