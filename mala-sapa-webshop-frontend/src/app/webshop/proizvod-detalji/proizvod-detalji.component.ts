import { Component, OnInit } from '@angular/core';
import { IProizvod } from 'src/app/shared/models/proizvodi';
import { WebshopService } from '../webshop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-proizvod-detalji',
  templateUrl: './proizvod-detalji.component.html',
  styleUrls: ['./proizvod-detalji.component.scss']
})
export class ProizvodDetaljiComponent implements OnInit {
  proizvod: IProizvod;

  constructor(private webShopService: WebshopService, private route: ActivatedRoute, private bcService: BreadcrumbService) {
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

}
