import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { NarudzbaSumaComponent } from './components/narudzba-suma/narudzba-suma.component';

@NgModule({
  declarations: [NarudzbaSumaComponent, NarudzbaSumaComponent],
  imports: [
    CommonModule,
    CarouselModule.forRoot(),
  ],
  exports: [
    CarouselModule,
    NarudzbaSumaComponent
  ]
})
export class SharedModule { }
