import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IKorisnik } from '../shared/models/korisnik';
import { KorisnickiRacunService } from '../korisnicki-racun/korisnicki-racun.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class NarudzbeService {
  baseUrl = environment.apiUrl;
  isAdmin: boolean;

  constructor(
    private http: HttpClient
  ) {}

  getNarudzbeZaKorisnika() {
    const storedIsAdmin = localStorage.getItem('isAdmin');
    this.isAdmin = storedIsAdmin === 'true';

    if (this.isAdmin) {
      return this.http.get(this.baseUrl + 'narudzba/admin', {
          params: { email: 'admin' },
        });
    } else {
      return this.http.get(this.baseUrl + 'narudzba');
    }
  }
  
  getNarudzba(id: number) {
    return this.http.get(this.baseUrl + 'narudzba/' + id);
  }
}
