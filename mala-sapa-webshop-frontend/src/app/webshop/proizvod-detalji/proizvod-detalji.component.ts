import { Component, OnInit } from '@angular/core';
import { IProizvod } from 'src/app/shared/models/proizvodi';
import { WebshopService } from '../webshop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-proizvod-detalji',
  templateUrl: './proizvod-detalji.component.html',
  styleUrls: ['./proizvod-detalji.component.scss']
})
export class ProizvodDetaljiComponent implements OnInit {
  proizvod: IProizvod;

  constructor(private webShopService: WebshopService, private route: ActivatedRoute ) { }

  ngOnInit(): void {
    this.ucitajProizvod();
  }

  ucitajProizvod() {
    this.webShopService.ucitajProizvod(+this.route.snapshot.paramMap.get('id')).subscribe(proizvod => {
      this.proizvod = proizvod;
    }, error => {
      console.log(error);
    });
  }

}
