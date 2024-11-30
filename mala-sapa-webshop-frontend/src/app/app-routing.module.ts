import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {path: '', component: PocetnaComponent, data: {breadcrumb: 'Početna'}},
  {path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test Errors'}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server Errors'}},
  {path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'Not Found'}},
  
  {path: 'webshop', loadChildren: () => import('./webshop/webshop.module').then(m => m.WebshopModule), data: {breadcrumb: 'Webshop'}},
  
  {path: 'kosarica', loadChildren: () => import('./kosarica/kosarica.module').then(m => m.KosaricaModule), data: {breadcrumb: 'Košarica'}},
  
  {
    path: 'blagajna', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./blagajna/blagajna.module').then(m => m.BlagajnaModule), data: {breadcrumb: 'Blagajna'}},
  
  {path: 'korisnicki-racun', loadChildren: () => import('./korisnicki-racun/korisnicki-racun.module').then(m => m.KorisnickiRacunModule), data: {breadcrumb: {skip: true}}},
  
  {path: 'test-error', component: TestErrorComponent },
  
  {path: '**', redirectTo: 'Nije pronađeno', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
