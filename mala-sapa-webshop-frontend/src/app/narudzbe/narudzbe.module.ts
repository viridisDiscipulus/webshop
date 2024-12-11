import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NarudzbeDetaljnoComponent } from './narudzbe-detaljno/narudzbe-detaljno.component';
import { SharedModule } from "../shared/shared.module";
import { NarudzbeRoutingModule } from './narudzbe-routing.module';



@NgModule({
  declarations: [NarudzbeDetaljnoComponent],
  imports: [
    CommonModule,
    SharedModule,
    NarudzbeRoutingModule
]
})
export class NarudzbeModule { }
