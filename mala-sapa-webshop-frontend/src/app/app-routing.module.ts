import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PocetnaComponent } from './pocetna/pocetna.component';

const routes: Routes = [
  {path: '', component: PocetnaComponent },
  {path: 'webshop', loadChildren: () => import('./webshop/webshop.module').then(m => m.WebshopModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
