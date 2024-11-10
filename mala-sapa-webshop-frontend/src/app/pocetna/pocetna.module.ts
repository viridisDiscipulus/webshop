import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocetnaComponent } from './pocetna.component';



@NgModule({
  declarations: [PocetnaComponent],
  imports: [
    CommonModule
  ],
  exports: [
    PocetnaComponent
  ]
})
export class PocetnaModule { }
