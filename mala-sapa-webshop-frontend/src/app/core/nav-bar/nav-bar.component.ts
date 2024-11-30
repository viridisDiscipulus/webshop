import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
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

  constructor(private kosaricaService: KosaricaService, private korisnickiRacun: KorisnickiRacunService) { }

  ngOnInit(): void {
    this.kosarica$ = this.kosaricaService.kosarica$;
    this.trenutniKorisnik$ = this.korisnickiRacun.trenutniKorisnik$;
  }

  odjava() {
    this.korisnickiRacun.odjava();
  }

}
