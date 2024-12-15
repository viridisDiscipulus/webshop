import { IvyParser } from '@angular/compiler';
import * as uuid from 'uuid';

export interface IKosarica {
    id: string;
    artikli: IArtiklKosarica[];
    nacinIsporukeID?: number;
}

export interface IArtiklKosarica {
    id: number;
    naziv: string;
    cijena: number;
    kolicina: number;
    slikaUrl: string;
    robnaMarka: string;
    vrstaProizvoda: string;
}


export class Kosarica implements IKosarica {
    id: string = uuid.v4(); // Generiraj UUID koristeci v4()
    artikli: IArtiklKosarica[] = [];
}

export interface IKosaricaUkupno {
    dostava: number;
    ukupno: number;
    ukupnaNarudzba: number;

}