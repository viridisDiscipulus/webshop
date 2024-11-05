import { Component, OnInit } from '@angular/core';
import { IProizvod } from '../shared/models/proizvodi';
import { WebshopService } from './webshop.service';

@Component({
  selector: 'app-webshop',
  templateUrl: './webshop.component.html',
  styleUrls: ['./webshop.component.scss']
})
export class WebshopComponent implements OnInit {
  proizvodi: IProizvod[];

  constructor(private webShopService: WebshopService) { }

  ngOnInit(): void {
    this.webShopService.ucitajSveProizvode().subscribe(response => {
      this.proizvodi = response;
    }, error => {
      console.log(error);
    });
  }

} 
