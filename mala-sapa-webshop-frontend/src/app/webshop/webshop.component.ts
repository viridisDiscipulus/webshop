import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProizvod } from '../shared/models/proizvodi';
import { WebshopService } from './webshop.service';
import { IRobnaMarka } from '../shared/models/robnaMarka';
import { IVrstaProizvoda } from '../shared/models/vrstaProizvoda';
import { WebshopParametri } from '../shared/models/webshopParametri';

@Component({
  selector: 'app-webshop',
  templateUrl: './webshop.component.html',
  styleUrls: ['./webshop.component.scss']
})
export class WebshopComponent implements OnInit {
  
  // However, using ElementRef directly can expose your application to security risks like Cross-Site Scripting (XSS) attacks, as it allows direct manipulation of the DOM. It is generally recommended to use Angular's built-in templating and data-binding features to interact with the DOM safely. If you must use ElementRef, ensure that any data inserted into the DOM is properly sanit

  @ViewChild('pretrazi', {static: false}) pretraziElement: ElementRef;
  proizvodi: IProizvod[];
  robneMarke: IRobnaMarka[];
  vrsteProizvoda: IVrstaProizvoda[];
  parametri = new WebshopParametri();

   opcijeSortiranja = [
    {name: 'A-Z', value: 'naziv'},
    {name: 'S višom cijenom', value: 'cijena desc'},
    {name: 'S nižom cijenom', value: 'cijena asc'}
  ];

  constructor(private webShopService: WebshopService) { }

  ngOnInit(): void {
   this.ucitajProizvode();
    this.ucitajRobneMarke();
    this.ucitajVrsteProizvoda(); 
  }

  ucitajProizvode() {
    this.webShopService.ucitajSveProizvode(this.parametri).subscribe(response => {
      this.proizvodi = response;
    }, error => {
      console.log(error);
    });
  }

  ucitajRobneMarke() {
    this.webShopService.ucitajSveRobneMarke().subscribe(response => {
      this.robneMarke = [{id: 0, naziv: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  ucitajVrsteProizvoda() {
    this.webShopService.ucitajSveVrsteProizvoda().subscribe(response => {
      this.vrsteProizvoda = [{id: 0, naziv: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onRobnaMarkaSelected(robnaMarkaId: number) {
    this.parametri.robnaMarkaId = robnaMarkaId;
    this.ucitajProizvode();
  }

  onVrstaProizvodaSelected(vrstaProizvodaId: number) {
    this.parametri.vrstaProizvodaId = vrstaProizvodaId;
    this.ucitajProizvode();
  }

  onSortiranjeSelected(sortiranje: string) {
    this.parametri.sortiranje = sortiranje;
    this.ucitajProizvode();
  }

  onPretrazi() {
    this.parametri.pretraga = this.pretraziElement.nativeElement.value;
    this.ucitajProizvode();
  }

  onPonisti() {
    this.pretraziElement.nativeElement.value = '';
    this.parametri = new WebshopParametri();
    this.ucitajProizvode();
  }

} 
