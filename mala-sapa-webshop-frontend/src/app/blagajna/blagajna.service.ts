import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { INacinIsporuke } from '../shared/models/nacinIsporuke';
import { map } from 'rxjs/operators';
import { INarudzbaZaKreiranje } from '../shared/models/narudzba';

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


}
