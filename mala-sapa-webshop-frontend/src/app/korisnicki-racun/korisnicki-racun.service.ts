import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IKorisnik } from '../shared/models/korisnik';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { IAdresa } from '../shared/models/adresa';

@Injectable({
  providedIn: 'root'
})
export class KorisnickiRacunService {
  baseUrl = environment.apiUrl;
  private trenutniKorisnikSource = new ReplaySubject<IKorisnik>(1);
  trenutniKorisnik$ = this.trenutniKorisnikSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  // getTrenutniKorisnik() {
  //   return this.trenutniKorisnikSource.value;
  // }

  ucitajTrenutnogKorisnika(token: string){
  if (token === null) {
    this.trenutniKorisnikSource.next(null);
    return of(null);
  }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get(this.baseUrl + 'korisnickiRacun', {headers}).pipe(
      map((korisnik: IKorisnik) => {
        if (korisnik) {
          localStorage.setItem('token', korisnik.token);
          this.trenutniKorisnikSource.next(korisnik);
        }
      })
    )
  }

  prijava(values: any) {
    return this.http.post(this.baseUrl + 'korisnickiRacun/prijava', values).pipe(
      map((korisnik: IKorisnik) => {
        if (korisnik) {
          localStorage.setItem('token', korisnik.token);
          this.trenutniKorisnikSource.next(korisnik);
        }
      })
    )
  }

  registracija(values: any) {
    return this.http.post(this.baseUrl + 'korisnickiRacun/registracija', values).pipe(
      map((korisnik: IKorisnik) => {
        if (korisnik) {
          localStorage.setItem('token', korisnik.token);
          this.trenutniKorisnikSource.next(korisnik);
        }
      })
    )
  }

  odjava() {
    localStorage.removeItem('token');
    this.trenutniKorisnikSource.next(null);
    this.router.navigateByUrl('/');
  }

  proveriEmail(email: string) {
    return this.http.get(this.baseUrl + 'korisnickiRacun/emailProvjera?email=' + email);
  }


  getKorisnikAdresa() {
    return this.http.get<IAdresa>(this.baseUrl + 'KorisnickiRacun/adresa');
  }

  azurirajKorisnikovuAdresu(adresa: IAdresa) {
    return this.http.post<IAdresa>(this.baseUrl + 'KorisnickiRacun/adresa', adresa);
  }
}
