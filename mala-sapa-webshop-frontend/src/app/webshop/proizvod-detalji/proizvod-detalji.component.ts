import { Component, OnInit } from '@angular/core';
import { IProizvod } from 'src/app/shared/models/proizvodi';
import { WebshopService } from '../webshop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';

@Component({
  selector: 'app-proizvod-detalji',
  templateUrl: './proizvod-detalji.component.html',
  styleUrls: ['./proizvod-detalji.component.scss']
})
export class ProizvodDetaljiComponent implements OnInit {
  proizvod: IProizvod;
  kolicina = 1;

  constructor(private webShopService: WebshopService, 
              private route: ActivatedRoute, 
              private bcService: BreadcrumbService,
              private kosaricaService: KosaricaService
              ) {
    this.bcService.set('@proizvodDetalji', '');
   }

  ngOnInit(): void {
    this.ucitajProizvod();
  }

  ucitajProizvod() {
    this.webShopService.ucitajProizvod(+this.route.snapshot.paramMap.get('id')).subscribe(proizvod => {
      this.proizvod = proizvod;
      // console.log('Proizvod Naziv:', proizvod.naziv);
      this.bcService.set('@proizvodDetalji', proizvod.naziv);
    }, error => {
      console.log(error);
    });
  }

  dodajUKosaricu() {
    this.kosaricaService.dodajArtiklUKosaricu(this.proizvod, this.kolicina);
  }

  uvecajKolicinu() {
    this.kolicina++;
  }

  umanjiKolicinu() {
    if (this.kolicina > 1) {
      this.kolicina--;
    }
  }

}
