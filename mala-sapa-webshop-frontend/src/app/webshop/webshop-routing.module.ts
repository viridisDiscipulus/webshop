import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WebshopComponent } from './webshop.component';
import { ProizvodDetaljiComponent } from './proizvod-detalji/proizvod-detalji.component';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

const routes: Routes = [
  {path: '', component: WebshopComponent},
  {path: ':id', component: ProizvodDetaljiComponent, data: {breadcrumb: {alias: 'proizvodDetalji'}}}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule, 
    RouterModule.forChild(routes) 
  ],
  exports: [RouterModule]
})
export class WebshopRoutingModule { }
