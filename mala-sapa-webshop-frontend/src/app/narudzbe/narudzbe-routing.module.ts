import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NarudzbeComponent } from './narudzbe.component';
import { NarudzbeDetaljnoComponent } from './narudzbe-detaljno/narudzbe-detaljno.component';

const routes: Routes = [
  { path: '', component: NarudzbeComponent },
  { path: ':id', component: NarudzbeDetaljnoComponent, data: {breadcrumb: {alias: 'Narudžba detaljno'}} },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NarudzbeRoutingModule {}
