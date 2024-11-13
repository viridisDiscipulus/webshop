import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocetnaComponent } from './pocetna.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [PocetnaComponent],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    PocetnaComponent
  ]
})
export class PocetnaModule { }
