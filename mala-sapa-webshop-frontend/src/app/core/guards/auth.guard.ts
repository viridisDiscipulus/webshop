import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { KorisnickiRacunService } from 'src/app/korisnicki-racun/korisnicki-racun.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private korisnickiRacunService: KorisnickiRacunService, private router: Router) {} 

  
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.korisnickiRacunService.trenutniKorisnik$.pipe(
      map(auth => {
        if (auth ) {
          return true;
        }
        this.router.navigate(['korisnicki-racun/prijava'], {queryParams: {returnUrl: state.url}});
      })
    )
  }
  
}
