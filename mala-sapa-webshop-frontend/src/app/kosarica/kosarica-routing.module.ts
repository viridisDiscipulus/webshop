import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { KosaricaComponent } from './kosarica.component';

const routes: Routes = [
  {path: '', component: KosaricaComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class KosaricaRoutingModule { }
