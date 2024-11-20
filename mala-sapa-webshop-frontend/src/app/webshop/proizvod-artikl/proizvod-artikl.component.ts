import { Component, Input, OnInit } from '@angular/core';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IProizvod } from 'src/app/shared/models/proizvodi';


@Component({
  selector: 'app-proizvod-artikl',
  templateUrl: './proizvod-artikl.component.html',
  styleUrls: ['./proizvod-artikl.component.scss' ]
})
export class ProizvodArtiklComponent implements OnInit {
  @Input() proizvod: IProizvod;
  
  constructor(private kosaricaService: KosaricaService) { }

  ngOnInit(): void {
  }

  dodajUKosaricu() {
    this.kosaricaService.dodajArtiklUKosaricu(this.proizvod);
  }

}
