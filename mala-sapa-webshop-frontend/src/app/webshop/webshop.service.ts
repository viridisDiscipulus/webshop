import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProizvod } from '../shared/models/proizvodi';
import { IRobnaMarka } from '../shared/models/robnaMarka';
import { IVrstaProizvoda } from '../shared/models/vrstaProizvoda';
import { map } from 'rxjs/operators';
import { WebshopParametri } from '../shared/models/webshopParametri';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WebshopService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  ucitajSveProizvode(parametri: WebshopParametri) {
    let params = new HttpParams();

    if (parametri.robnaMarkaId) {
      params = params.append('robnaMarkaId', parametri.robnaMarkaId.toString());
    }

    if (parametri.vrstaProizvodaId) {
      params = params.append('vrstaProizvodaId', parametri.vrstaProizvodaId.toString());
    }

    if (parametri.sortiranje) {
      params = params.append('sortiranje', parametri.sortiranje);
    }

    if (parametri.pretraga) {
      params = params.append('pretraga', parametri.pretraga);
    }

    return this.http.get<IProizvod[]>(this.baseUrl + 'Proizvod/SviProizvodi', { observe: 'response', params }).pipe(
      map(response => {
        return response.body;
      })
    )
  }

  ucitajProizvod(id: number) {
    let proizvod: any;
    proizvod = this.http.get<IProizvod>(this.baseUrl + 'Proizvod/' + id);
    return proizvod;
  }

  ucitajSveRobneMarke() {
    return this.http.get<IRobnaMarka[]>(this.baseUrl + 'Proizvod/RobneMarke');
  }

  ucitajSveVrsteProizvoda() {
    return this.http.get<IVrstaProizvoda[]>(this.baseUrl + 'Proizvod/VrsteProizvoda');
  }
}
