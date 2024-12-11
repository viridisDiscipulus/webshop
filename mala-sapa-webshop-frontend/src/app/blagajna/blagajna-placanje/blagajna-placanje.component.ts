import { Component, Input, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IKosarica } from 'src/app/shared/models/kosarica';
import { BlagajnaService } from '../blagajna.service';
import { INarudzba, INarudzbaZaKreiranje } from 'src/app/shared/models/narudzba';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-blagajna-placanje',
  templateUrl: './blagajna-placanje.component.html',
  styleUrls: ['./blagajna-placanje.component.scss']
})
export class BlagajnaPlacanjeComponent implements OnInit {
  @Input() blagajnaForm: FormGroup;
   ucitavanje: boolean;

  constructor(
      private kosaricaService: KosaricaService, 
      private blagajnaService: BlagajnaService, 
      private toastr: ToastrService, 
      private router: Router) { }

    ngOnInit(): void {
    }

  async predajNarudzbu() {
    try {
      const kosarica = this.kosaricaService.getKosaricaValue();
      const narudzbaZaKreiranje = await this.kreirajObjektNarudzbe(kosarica);
      this.blagajnaService.kreirajNarudzbu(narudzbaZaKreiranje).subscribe((narudzba: INarudzba) => {
        this.toastr.success('Narudžba uspješno kreirana');
        this.kosaricaService.obrisiKosaricu(kosarica);
        const navigationExtras: NavigationExtras = {state: narudzba};
        this.router.navigate(['blagajna/blagajna-zavrsetak-procesa'], navigationExtras);
      }, error => {
        this.toastr.error('Narudžba nije uspješno kreirana');
        console.log(error);
      });
    } catch (error) {
      this.toastr.error('Došlo je do greške prilikom kreiranja narudžbe');
      console.log(error);
    }
  }

  private async kreirajObjektNarudzbe(kosarica: IKosarica) {
    try {
      return {
        kosaricaId: kosarica.id,
        nacinIsporukeId: +this.blagajnaForm.get('nacinIsporukeForm').get('nacinIsporuke').value,
        adresaDostave: this.blagajnaForm.get('adresaForm').value,
      };
    } catch (error) {
      this.toastr.error('Došlo je do greške prilikom kreiranja objekta narudžbe');
      console.log(error);
      throw error;
    }
  }
}