import { Component, Input, OnInit } from '@angular/core';
import { IArtiklKosarica, IKosarica } from '../models/kosarica';
import { EventEmitter, Output } from '@angular/core';
import { INaruceniArtikl } from '../models/narudzba';

@Component({
  selector: 'app-kosarica-pregled',
  templateUrl: './kosarica-pregled.component.html',
  styleUrls: ['./kosarica-pregled.component.scss']
})
export class KosaricaPregledComponent implements OnInit {
  @Output() umanjiKolicinu: EventEmitter<IArtiklKosarica> = new EventEmitter<IArtiklKosarica>();
  @Output() uvecajKolicinu: EventEmitter<IArtiklKosarica> = new EventEmitter<IArtiklKosarica>();
  @Output() ukloniArtikl: EventEmitter<IArtiklKosarica> = new EventEmitter<IArtiklKosarica>();
  @Input() kosaricaBool = true; 
  @Input() artikli: IArtiklKosarica[] | INaruceniArtikl[] = [];
  @Input() narudzbaBool = false;


  constructor() { }

  ngOnInit(): void {
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
