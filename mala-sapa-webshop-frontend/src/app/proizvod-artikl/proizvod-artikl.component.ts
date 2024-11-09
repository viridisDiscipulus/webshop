import { Component, Input, OnInit } from '@angular/core';
import { IProizvod } from '../shared/models/proizvodi';

@Component({
  selector: 'app-proizvod-artikl',
  templateUrl: './proizvod-artikl.component.html',
  styleUrls: ['./proizvod-artikl.component.scss']
})
export class ProizvodArtiklComponent implements OnInit {
  @Input() proizvod: IProizvod;
  
  constructor() { }

  ngOnInit(): void {
  }

}
