import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { KorisnickiRacunService } from 'src/app/korisnicki-racun/korisnicki-racun.service';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IKorisnik } from 'src/app/shared/models/korisnik';
import { IKosarica } from 'src/app/shared/models/kosarica';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  kosarica$: Observable<IKosarica>;
  trenutniKorisnik$: Observable<IKorisnik>;
  isAdmin: boolean = false;

  constructor(private kosaricaService: KosaricaService, private korisnickiRacun: KorisnickiRacunService) { }

  ngOnInit(): void {
    this.kosarica$ = this.kosaricaService.kosarica$;
    this.trenutniKorisnik$ = this.korisnickiRacun.trenutniKorisnik$;

    this.trenutniKorisnik$.pipe(
      map(korisnik => korisnik && korisnik.alias === 'admin')
    ).subscribe(isAdmin => {
      this.isAdmin = isAdmin;
      if (isAdmin !== null) {
        localStorage.setItem('isAdmin', isAdmin.toString());
      }
    });
  }

  odjava() {
    this.korisnickiRacun.odjava();
    localStorage.removeItem('isAdmin');
    localStorage.removeItem('nacinIsporukeID');
  }

}
