import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KosaricaComponent } from './kosarica.component';
import { KosaricaRoutingModule } from './kosarica-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [KosaricaComponent],
  imports: [
    CommonModule,
    KosaricaRoutingModule,
    SharedModule
  ]
})
export class KosaricaModule { }
