import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { KorisnickiRacunService } from 'src/app/korisnicki-racun/korisnicki-racun.service';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IAdresa } from 'src/app/shared/models/adresa';
import { IKosaricaUkupno } from 'src/app/shared/models/kosarica';


@Component({
  selector: 'app-blagajna-adresa',
  templateUrl: './blagajna-adresa.component.html',
  styleUrls: ['./blagajna-adresa.component.scss']
})
export class BlagajnaAdresaComponent implements OnInit {
  @Input() blagajnaForm: FormGroup;
  kosaricaUkupno$: Observable<IKosaricaUkupno>;

  constructor(private fb: FormBuilder, private korisnickiRacunService: KorisnickiRacunService, private toastr: ToastrService, private kosaricaService: KosaricaService) { }

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
      placanjeForma: this.fb.group({
        vlasnikKartice: [null, Validators.required]
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
    const kosarica = this.kosaricaService.getKosaricaValue();
    if (kosarica !== null) {
      // this.blagajnaForm.get('nacinIsporukeForm').get('nacinIsporuke').patchValue(kosarica.artiklitoString());
      // console.log(this.blagajnaForm.get('nacinIsporukeForm').get('nacinIsporuke'));
    }
  }

  spremiKorisnckuAdresu() {
    this.korisnickiRacunService.azurirajKorisnikovuAdresu(this.blagajnaForm.get('adresaForm').value)
      .subscribe((adresa: IAdresa) => {
        this.toastr.success('Adresa spremljena');
        this.blagajnaForm.get('adresaForm').reset(adresa);
      }, error => {
        this.toastr.error(error.message);
        console.log(error);
      });
  }
}
