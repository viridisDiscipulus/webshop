import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlagajnaComponent } from './blagajna.component';
import { BlagajnaRoutingModule } from './blagajna-routing.module';



@NgModule({
  declarations: [BlagajnaComponent],
  imports: [
    CommonModule,
    BlagajnaRoutingModule
  ]
})
export class BlagajnaModule { }
