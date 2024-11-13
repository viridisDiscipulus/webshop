import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PocetnaComponent } from './pocetna.component';
import { SharedModule } from '../shared/shared.module';
import { CoreModule } from "../core/core.module";



@NgModule({
  declarations: [PocetnaComponent],
  imports: [
    CommonModule,
    SharedModule,
    CoreModule
],
  exports: [
    PocetnaComponent
  ]
})
export class PocetnaModule { }
