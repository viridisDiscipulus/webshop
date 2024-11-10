import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';

const routes: Routes = [
  {path: '', component: PocetnaComponent },
  {path: 'test-error', component: TestErrorComponent },
  {path: 'server-error', component: ServerErrorComponent },
  {path: 'not-found', component: NotFoundComponent },
  {path: 'webshop', loadChildren: () => import('./webshop/webshop.module').then(m => m.WebshopModule)},
  {path: 'test-error', component: TestErrorComponent },
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
