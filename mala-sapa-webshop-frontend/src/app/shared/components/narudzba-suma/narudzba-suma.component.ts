import { Component, Input, OnInit } from '@angular/core';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';

@Component({
  selector: 'app-narudzba-suma',
  templateUrl: './narudzba-suma.component.html',
  styleUrls: ['./narudzba-suma.component.scss']
})
export class NarudzbaSumaComponent implements OnInit {
  kosaricaUkupno$ = this.kosaricaService.kosaricaUkupno$;
  @Input() narudzbaDostava: number;
  @Input() narudzbaCijena: number;
  @Input() narudzbaUkupno: number;

  constructor(
        private kosaricaService: KosaricaService
          ) { }

  ngOnInit(): void {
    this.kosaricaUkupno$ = this.kosaricaService.kosaricaUkupno$;
  }

}
