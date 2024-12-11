import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NarudzbeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getNarudzbeZaKorisnika() {
    return this.http.get(this.baseUrl + 'narudzba');
  }

  getNarudzba(id: number) {
    return this.http.get(this.baseUrl + 'narudzba/' + id);
  }
}
