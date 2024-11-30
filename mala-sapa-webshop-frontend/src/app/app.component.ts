import { Component, OnInit } from '@angular/core';
import { KosaricaService } from './kosarica/kosarica.service';
import { KorisnickiRacunModule } from './korisnicki-racun/korisnicki-racun.module';
import { KorisnickiRacunService } from './korisnicki-racun/korisnicki-racun.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Mala Šapa';

  constructor(private kosaricaService: KosaricaService, private korisnickiRacunService: KorisnickiRacunService) { }

  ngOnInit(): void {
    this.ucitajKosaricu();  
    this.ucitajTrenutnogKorisnika();
  }

  ucitajTrenutnogKorisnika() { 
    const token = localStorage.getItem('token');
      this.korisnickiRacunService.ucitajTrenutnogKorisnika(token).subscribe(() => {
        console.log('Ucitani podaci korisnika iz local storage');
      }, error => {
        console.log(error);
      });
  }

  ucitajKosaricu() {
    const kosaricaId = localStorage.getItem('kosarica_id');
    if (kosaricaId) {
      this.kosaricaService.getKosarica(kosaricaId).subscribe(() => {
        console.log('Ucitana kosarica iz local storage');
      }, error => {
        console.log(error);
      });
    }
  }
}
