import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NarudzbaSumaComponent } from './components/narudzba-suma/narudzba-suma.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from './components/stepper/stepper.component';
import { KosaricaPregledComponent } from './kosarica-pregled/kosarica-pregled.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [NarudzbaSumaComponent, TextInputComponent, StepperComponent, KosaricaPregledComponent],
  imports: [
    CommonModule,
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    CdkStepperModule,
    RouterModule
  ],
  exports: [
    CarouselModule,
    NarudzbaSumaComponent,
    BsDropdownModule,
    ReactiveFormsModule,
    TextInputComponent,
    CdkStepperModule,
    StepperComponent,
    KosaricaPregledComponent
  ]
})
export class SharedModule { }
