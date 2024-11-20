import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BlagajnaComponent } from './blagajna.component';

const routes: Routes = [
  {path: '', component: BlagajnaComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class BlagajnaRoutingModule { }
