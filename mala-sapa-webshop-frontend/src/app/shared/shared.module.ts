import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NarudzbaSumaComponent } from './components/narudzba-suma/narudzba-suma.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
  declarations: [NarudzbaSumaComponent, TextInputComponent],
  imports: [
    CommonModule,
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule
  ],
  exports: [
    CarouselModule,
    NarudzbaSumaComponent,
    BsDropdownModule,
    ReactiveFormsModule,
    TextInputComponent
  ]
})
export class SharedModule { }
