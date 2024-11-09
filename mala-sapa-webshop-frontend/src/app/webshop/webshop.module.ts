import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebshopComponent } from './webshop.component';
import { ProizvodArtiklComponent } from '../proizvod-artikl/proizvod-artikl.component';


@NgModule({
  declarations: [
    WebshopComponent,
    ProizvodArtiklComponent
  ],
  imports: [
    CommonModule,

  ],
  exports: [
    WebshopComponent
  ]
})
export class WebshopModule { }
