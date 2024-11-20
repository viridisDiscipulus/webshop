import * as uuid from 'uuid';

export interface IKosarica {
    id: string;
    artikli: IArtiklKosarica[];
}

export interface IArtiklKosarica {
    id: number;
    proizvodNaziv: string;
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