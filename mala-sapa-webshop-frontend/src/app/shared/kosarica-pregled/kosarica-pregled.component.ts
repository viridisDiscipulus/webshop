import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IArtiklKosarica, IKosarica } from '../models/kosarica';
import { EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-kosarica-pregled',
  templateUrl: './kosarica-pregled.component.html',
  styleUrls: ['./kosarica-pregled.component.scss']
})
export class KosaricaPregledComponent implements OnInit {
  kosarica$: Observable<IKosarica>;
  @Output() umanjiKolicinu: EventEmitter<IArtiklKosarica> = new EventEmitter<IArtiklKosarica>();
  @Output() uvecajKolicinu: EventEmitter<IArtiklKosarica> = new EventEmitter<IArtiklKosarica>();
  @Output() ukloniArtikl: EventEmitter<IArtiklKosarica> = new EventEmitter<IArtiklKosarica>();
  @Input() kosaricaBool = true; 


  constructor(private kosaricaService: KosaricaService) { }

  ngOnInit(): void {
    this.kosarica$ = this.kosaricaService.kosarica$;
  }

  ukloniArtiklIzKosarice(artikl: IArtiklKosarica) {
    this.ukloniArtikl.emit(artikl);
    }

  uvecajKolicinuArtikla(artikl: IArtiklKosarica) {
    this.uvecajKolicinu.emit(artikl);
    }

    umanjiKolicinuArtikla(artikl: IArtiklKosarica) {
    this.umanjiKolicinu.emit(artikl);
    }



}
