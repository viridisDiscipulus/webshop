import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BlagajnaComponent } from './blagajna.component';
import { BlagajnaZavrsetakProcesaComponent } from './blagajna-zavrsetak-procesa/blagajna-zavrsetak-procesa.component';

const routes: Routes = [
  {path: '', component: BlagajnaComponent},
  {path: 'blagajna-zavrsetak-procesa', component: BlagajnaZavrsetakProcesaComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class BlagajnaRoutingModule { }
