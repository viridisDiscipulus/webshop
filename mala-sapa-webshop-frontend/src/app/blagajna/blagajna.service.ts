import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { INacinIsporuke } from '../shared/models/nacinIsporuke';
import { map } from 'rxjs/operators';
import { INarudzbaZaKreiranje, IPlacanje } from '../shared/models/narudzba';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BlagajnaService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getNacinIsporuke() {
    return this.http.get(this.baseUrl + 'narudzba/nacinIsporuke').pipe(
      map((response: INacinIsporuke[]) => {
        return response.sort((a, b) => a.id - b.id);
      })
    );
  }

  kreirajNarudzbu(narudzba: INarudzbaZaKreiranje) {
    return this.http.post(this.baseUrl + 'narudzba', narudzba);
  }

  provjeriPodatkePlacanja(placanje: IPlacanje): Observable<IPlacanje> {
    const params = new HttpParams()
      .set('vlasnikKartice', placanje.vlasnikKartice)
      .set('brojKartice', placanje.brojKartice)
      .set('datumIsteka', placanje.datumIsteka)
      .set('cvv', placanje.cvv);
  
    return this.http.get<IPlacanje>(this.baseUrl + 'narudzba/placanje', { params });
  }


}
