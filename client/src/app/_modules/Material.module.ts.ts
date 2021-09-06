//3rd Party modules used by the application 
import { NgModule } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field'; 
import {NoopAnimationsModule} from '@angular/platform-browser/animations';
import {MatSelectModule} from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button'; 
import {MatPaginatorModule} from '@angular/material/paginator'; 
import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatIconModule} from '@angular/material/icon'; 


@NgModule({
  declarations: [],
  imports: [
    MatFormFieldModule,
    NoopAnimationsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatButtonModule,
    MatPaginatorModule,
    MatToolbarModule,
    MatIconModule
    
  ],
  exports: [
    MatFormFieldModule,
    NoopAnimationsModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatPaginatorModule,
    MatToolbarModule,
    MatIconModule
  ]
})
export class MaterialModules { }
