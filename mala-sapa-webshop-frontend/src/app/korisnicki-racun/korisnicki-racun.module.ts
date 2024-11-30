import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrijavaComponent } from './prijava/prijava.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { KorisnickiRacunRoutingModule } from './korisnicki-racun-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [PrijavaComponent, RegistracijaComponent],
  imports: [
    CommonModule,
    KorisnickiRacunRoutingModule,
    SharedModule
  ]
})
export class KorisnickiRacunModule { }
