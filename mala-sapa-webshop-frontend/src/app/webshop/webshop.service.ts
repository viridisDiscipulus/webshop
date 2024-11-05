import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProizvod } from '../shared/models/proizvodi';

@Injectable({
  providedIn: 'root'
})
export class WebshopService {
  baseUrl = 'http://localhost:5000/api/';

  constructor(private http: HttpClient) { }

  ucitajSveProizvode() {
    return this.http.get<IProizvod[]>(this.baseUrl + 'Proizvod/SviProizvodi');
  }
}
