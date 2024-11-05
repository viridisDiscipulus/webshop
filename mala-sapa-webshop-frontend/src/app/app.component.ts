import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProizvod } from './models/proizvodi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Mala Šapa';
  proizvodi: IProizvod[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('http://localhost:5000/api/Proizvod/SviProizvodi').subscribe((response: any) => {
      this.proizvodi = response;
    }, error => {
      console.log(error);
    });
  }

}
