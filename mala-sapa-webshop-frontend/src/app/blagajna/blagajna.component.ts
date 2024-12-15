import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { KorisnickiRacunService } from '../korisnicki-racun/korisnicki-racun.service';
import { KosaricaService } from '../kosarica/kosarica.service';
import { IKosaricaUkupno } from '../shared/models/kosarica';

@Component({
  selector: 'app-blagajna',
  templateUrl: './blagajna.component.html',
  styleUrls: ['./blagajna.component.scss']
})
export class BlagajnaComponent implements OnInit {
  kosaricaUkupno$: Observable<IKosaricaUkupno>;
  blagajnaForm: FormGroup;

  constructor(private fb: FormBuilder, private korisnickiRacunService: KorisnickiRacunService, private kosaricaService: KosaricaService) { }

  ngOnInit() {
    this.createBlagajnaForm();
    this.getAdresaPodaci();
    this.getNacinDostavePodaci();
    this.kosaricaUkupno$ = this.kosaricaService.kosaricaUkupno$;
  }

  createBlagajnaForm() {
    this.blagajnaForm = this.fb.group({
      adresaForm: this.fb.group({
        ime: [null, Validators.required],
        prezime: [null, Validators.required],
        ulica: [null, Validators.required],
        grad: [null, Validators.required],
        drzava: [null, Validators.required],
        postanskiBroj: [null, Validators.required],
      }),
      nacinIsporukeForm: this.fb.group({
        nacinIsporuke: [null, Validators.required]
      }),
      placanjeForm: this.fb.group({
        vlasnikKartice: [null, Validators.required],
        brojKartice: [null, Validators.required],
        datumIsteka: [null, Validators.required],
        cvv: [null, Validators.required]
      })
    });
  }

  getAdresaPodaci() {
    this.korisnickiRacunService.getKorisnikAdresa().subscribe(adresa => {
      if (adresa) {
        this.blagajnaForm.get('adresaForm').patchValue(adresa);
      }
    }, error => {
      console.log(error);
    });
  }

  getNacinDostavePodaci() {
    const nacinIsporukeID = localStorage.getItem('nacinIsporukeID');
    if (nacinIsporukeID) {
      this.blagajnaForm.get('nacinIsporukeForm').get('nacinIsporuke').patchValue(nacinIsporukeID);
    }
  }
}