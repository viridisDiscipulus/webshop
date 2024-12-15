import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BlagajnaService } from '../blagajna.service';
import { INacinIsporuke } from 'src/app/shared/models/nacinIsporuke';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';

@Component({
  selector: 'app-blagajna-dostava',
  templateUrl: './blagajna-dostava.component.html',
  styleUrls: ['./blagajna-dostava.component.scss']
})
export class BlagajnaDostavaComponent implements OnInit {

  @Input() blagajnaForm: FormGroup;
  naciniIsporuke: INacinIsporuke[];
  napomene: string[] = []; 

  constructor(private blagajnaService: BlagajnaService, private kosaricaService: KosaricaService) { }

  ngOnInit(): void {
    this.blagajnaService.getNacinIsporuke().subscribe((response: INacinIsporuke[]) => {
      this.naciniIsporuke = response;
    }, error => {
      console.log(error);
    });
  }

  postaviCijenuDostave(nacinDostave: INacinIsporuke) {
    this.kosaricaService.postaviCijenuDostave(nacinDostave);
    this.saveNacinIsporukeToLocalStorage(nacinDostave.id);
  }

  saveNacinIsporukeToLocalStorage(nacinIsporukeID: number) {
    localStorage.setItem('nacinIsporukeID', nacinIsporukeID.toString());
  }

  onDodajNapomenu(napomena: string): void {
    this.napomene.push(napomena);
  }

 
}
