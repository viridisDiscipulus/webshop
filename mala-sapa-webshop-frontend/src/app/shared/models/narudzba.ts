import { IAdresa } from "./adresa"

export interface INarudzbaZaKreiranje {
    kosaricaId: string
    nacinIsporukeId: number
    adresaDostave: IAdresa
    placanje: IPlacanje
  }
  
  export interface INarudzba {
    id: number
    kupacEmail: string
    datumNarudzbe: string
    adresaDostave: IAdresa
    nacinIsporuke: string
    cijenaDostave: number
    naruceniArtikli: INaruceniArtikl[]
    ukupnaCijena: number
    status: string
    sveukupnaCijena: number
  }
  
  export interface INaruceniArtikl {
    proizvodId: number
    proizvodNaziv: string
    slikaUrl: string
    cijena: number
    kolicina: number
  }

  export interface IPlacanje {
    vlasnikKartice: string;
    brojKartice: string;
    datumIsteka: string;
    cvv: string;
}