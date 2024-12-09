import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { INarudzba } from 'src/app/shared/models/narudzba';

@Component({
  selector: 'app-blagajna-zavrsetak-procesa',
  templateUrl: './blagajna-zavrsetak-procesa.component.html',
  styleUrls: ['./blagajna-zavrsetak-procesa.component.scss']
})
export class BlagajnaZavrsetakProcesaComponent implements OnInit {
  narudzba: INarudzba;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation && navigation.extras && navigation.extras.state;
    if (state) {
      this.narudzba = this.narudzba as INarudzba;
    }
   }

  ngOnInit(): void {
  }

}
