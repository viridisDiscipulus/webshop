import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IArtiklKosarica, IKosarica, IKosaricaUkupno, Kosarica } from '../shared/models/kosarica';
import { map } from 'rxjs/operators';
import { IProizvod } from '../shared/models/proizvodi';
import { INacinIsporuke } from '../shared/models/nacinIsporuke';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class KosaricaService {
  baseUrl = environment.apiUrl
  private kosaricaSource = new BehaviorSubject<IKosarica>(null);
  kosarica$ = this.kosaricaSource.asObservable();
  private kosaricaUkupnoSource = new BehaviorSubject<IKosaricaUkupno>(null);
  kosaricaUkupno$ = this.kosaricaUkupnoSource.asObservable();
  isporuka: number = 0;

  constructor(private http: HttpClient) {}

  getKosarica(id: string) {
    return this.http
      .get(this.baseUrl + 'kosarica?id=' + id)
      .pipe(map((kosarica: IKosarica) => {
        this.kosaricaSource.next(kosarica);
        // console.log(this.getKosaricaValue());
        this.izracunajSumu();
      }));
    }

  setKosarica(kosarica: IKosarica) {
    return this.http
      .post(this.baseUrl + 'kosarica', kosarica)
      .subscribe((response: IKosarica) => {
        this.kosaricaSource.next(response);
        // console.log(response);
        this.izracunajSumu();
      }, error => {
        console.log(error);
      });
  }

  getKosaricaValue() {
    return this.kosaricaSource.value;
  }

  dodajArtiklUKosaricu(artikl: IProizvod, kolicina = 1) {
    const artiklZaDodati = this.mapProizvodArtiklToKosaricaArtikl(artikl, kolicina);
    const kosarica = this.getKosaricaValue() ?? this.kreirajKosaricu();
    // console.log(kosarica);
    kosarica.artikli = this.dodajIliAzurirajArtikl(kosarica.artikli, artiklZaDodati, kolicina);
    this.setKosarica(kosarica);
  }

  dodajIliAzurirajArtikl(artikli: IArtiklKosarica[], artiklZaDodati: IArtiklKosarica, kolicina: number): IArtiklKosarica[] {
    // console.log(artikli);
    const index = artikli.findIndex(x => x.id === artiklZaDodati.id);
    if (index === -1) {
      artikli.push(artiklZaDodati);
    } else {
      artikli[index].kolicina += kolicina;
    }
    return artikli;
  }

  uvecajKolicinuArtiklaUKosarici(artikl: IArtiklKosarica){
    const kosarica = this.getKosaricaValue();
    const pronadenArtiklIndex = kosarica.artikli.findIndex(a => a.id === artikl.id);
    kosarica.artikli[pronadenArtiklIndex].kolicina++;
    this.setKosarica(kosarica);
  }

  umanjiKolicinuArtiklaUKosarici(artikl: IArtiklKosarica){
    const kosarica = this.getKosaricaValue();
    const pronadenArtiklIndex = kosarica.artikli.findIndex(a => a.id === artikl.id);
    if (kosarica.artikli[pronadenArtiklIndex].kolicina > 1) {
      kosarica.artikli[pronadenArtiklIndex].kolicina--;
      this.setKosarica(kosarica);
    } else {
      this.ukloniArtiklIzKosarice(artikl);
    }
  }

  ukloniArtiklIzKosarice(artikl: IArtiklKosarica){
    const kosarica = this.getKosaricaValue();
    if (kosarica.artikli.some(a => a.id === artikl.id)){
      kosarica.artikli = kosarica.artikli.filter(a => a.id !== artikl.id);
      if (kosarica.artikli.length > 0){
        this.setKosarica(kosarica);
      } else {
        this.obrisiKosaricu(kosarica);
      }
    }
  }

  obrisiKosaricu(kosarica: IKosarica){
    return this.http.delete(this.baseUrl + 'kosarica?id=' + kosarica.id).subscribe(() => {
      this.kosaricaSource.next(null);
      this.kosaricaUkupnoSource.next(null);
      localStorage.removeItem('kosarica_id');
    }, error => {
      console.log(error);
    });
  }

  private kreirajKosaricu(): IKosarica {
    const kosarica = new Kosarica();
    localStorage.setItem('kosarica_id', kosarica.id);
    return kosarica; 
  }

  private mapProizvodArtiklToKosaricaArtikl(artikl: IProizvod, kolicina: number): IArtiklKosarica {
    return {
      id: artikl.id,
      naziv: artikl.naziv,
      cijena: artikl.cijena,
      slikaUrl: artikl.slikaUrl,
      kolicina: kolicina,
      robnaMarka: artikl.robnaMarka,
      vrstaProizvoda: artikl.vrstaProizvoda
    };
  }

  private izracunajSumu(){
    const kosarica = this.getKosaricaValue();
    const dostava = this.isporuka;
    const ukupno = kosarica.artikli.reduce((prev, curr) => {
      return prev + curr.kolicina * curr.cijena;
    }, 0);
    const ukupnaNarudzba = ukupno + dostava;
    this.kosaricaUkupnoSource.next({dostava, ukupno, ukupnaNarudzba});
  }

  postaviCijenuDostave(nacinIsporuke: INacinIsporuke) {
    this.isporuka = nacinIsporuke.cijena;
    const kosarica = this.getKosaricaValue();
    kosarica.nacinIsporukeID = nacinIsporuke.id;
    this.izracunajSumu();
    this.setKosarica (kosarica);
  }
}
