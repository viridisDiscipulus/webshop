import { Component, OnInit } from '@angular/core';
import { INarudzba } from '../shared/models/narudzba';
import { NarudzbeService } from './narudzbe.service';

@Component({
  selector: 'app-narudzbe',
  templateUrl: './narudzbe.component.html',
  styleUrls: ['./narudzbe.component.scss']
})
export class NarudzbeComponent implements OnInit {
  narudzbe: INarudzba[];

  constructor(private narudzbeService: NarudzbeService) { }

  ngOnInit(): void {
    this.getNarudzbe();
  }

  getNarudzbe(){
    this.narudzbeService.getNarudzbeZaKorisnika().subscribe((response: INarudzba[] )=> {
      this.narudzbe = response;
      // console.log(response);
    }, error => {
      console.log(error);
    });
  }
}