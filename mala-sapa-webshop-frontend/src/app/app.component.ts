import { Component, OnInit } from '@angular/core';
import { KosaricaService } from './kosarica/kosarica.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Mala Šapa';

  constructor(private kosaricaService: KosaricaService) { }

  ngOnInit(): void {
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
