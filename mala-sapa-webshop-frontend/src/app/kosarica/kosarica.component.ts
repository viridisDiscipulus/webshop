import { Component, OnInit } from '@angular/core';
import { KosaricaService } from './kosarica.service';
import { Observable } from 'rxjs';
import { IArtiklKosarica, IKosarica } from '../shared/models/kosarica';

@Component({
  selector: 'app-kosarica',
  templateUrl: './kosarica.component.html',
  styleUrls: ['./kosarica.component.scss']
})
export class KosaricaComponent implements OnInit {
  kosarica$: Observable<IKosarica>;

  constructor(private kosaricaSerive: KosaricaService) { }

  ngOnInit(): void {
    this.kosarica$ = this.kosaricaSerive.kosarica$;
  }

  ukloniArtiklIzKosarice(artikl: IArtiklKosarica) {
    this.kosaricaSerive.ukloniArtiklIzKosarice(artikl);
  }

  uvecajKolicinuArtikla(artikl: IArtiklKosarica) {
    this.kosaricaSerive.uvecajKolicinuArtiklaUKosarici(artikl);
  }

  umanjiKolicinuArtikla(artikl: IArtiklKosarica) {
    this.kosaricaSerive.umanjiKolicinuArtiklaUKosarici(artikl);
  }



}
