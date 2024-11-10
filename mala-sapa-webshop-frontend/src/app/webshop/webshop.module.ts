import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebshopComponent } from './webshop.component';
import { SharedModule } from '../shared/shared.module';
import { ProizvodArtiklComponent } from './proizvod-artikl/proizvod-artikl.component';
import { ProizvodDetaljiComponent } from './proizvod-detalji/proizvod-detalji.component';
import { WebshopRoutingModule } from './webshop-routing.module';

@NgModule({
  declarations: [
    WebshopComponent,
    ProizvodArtiklComponent,
    ProizvodDetaljiComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    WebshopRoutingModule
  ],
  exports: [
    ]
})
export class WebshopModule { }
