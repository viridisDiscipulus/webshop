import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlagajnaComponent } from './blagajna.component';
import { BlagajnaRoutingModule } from './blagajna-routing.module';
import { SharedModule } from "../shared/shared.module";
import { BlagajnaAdresaComponent } from './blagajna-adresa/blagajna-adresa.component';
import { BlagajnaDostavaComponent } from './blagajna-dostava/blagajna-dostava.component';
import { BlagajnaPregledComponent } from './blagajna-pregled/blagajna-pregled.component';
import { BlagajnaPlacanjeComponent } from './blagajna-placanje/blagajna-placanje.component';
import { BlagajnaZavrsetakProcesaComponent } from './blagajna-zavrsetak-procesa/blagajna-zavrsetak-procesa.component';



@NgModule({
  declarations: [BlagajnaComponent, BlagajnaAdresaComponent, BlagajnaDostavaComponent, BlagajnaPregledComponent, BlagajnaPlacanjeComponent, BlagajnaZavrsetakProcesaComponent],
  imports: [
    CommonModule,
    BlagajnaRoutingModule,
    SharedModule
]
})
export class BlagajnaModule { }
