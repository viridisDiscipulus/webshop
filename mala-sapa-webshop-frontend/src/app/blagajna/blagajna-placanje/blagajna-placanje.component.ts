import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IKosarica } from 'src/app/shared/models/kosarica';
import { BlagajnaService } from '../blagajna.service';
import { INarudzba, INarudzbaZaKreiranje, IPlacanje } from 'src/app/shared/models/narudzba';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-blagajna-placanje',
  templateUrl: './blagajna-placanje.component.html',
  styleUrls: ['./blagajna-placanje.component.scss']
})
export class BlagajnaPlacanjeComponent implements AfterViewInit {
  @Input() blagajnaForm: FormGroup;
  placanjeData: IPlacanje;
  preduvjetiZaNarudzbu: boolean;

  constructor(
    private kosaricaService: KosaricaService,
    private blagajnaService: BlagajnaService,
    private toastr: ToastrService,
    private router: Router) { }


  ngAfterViewInit(): void {
  }


  async predradnjeZaNarudzbu() {
    if (!this.provjeriUnosPodatakaKartice()) {
      return;
    }

    await this.provjeriPodatkeSaPlatnimSustavom();

    if (!this.preduvjetiZaNarudzbu) {
      return;
    } else {
      this.predajNarudzbu();
    }
  }

  async predajNarudzbu() {
    try {
      const kosarica = this.kosaricaService.getKosaricaValue();
      const narudzbaZaKreiranje = await this.kreirajObjektNarudzbe(kosarica);
      this.blagajnaService.kreirajNarudzbu(narudzbaZaKreiranje).subscribe((narudzba: INarudzba) => {
        this.toastr.success('Narudžba uspješno kreirana');
        this.kosaricaService.obrisiKosaricu(kosarica);
        const navigationExtras: NavigationExtras = { state: narudzba };
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
        placanje: this.blagajnaForm.get('placanjeForm').value,
      };
    } catch (error) {
      this.toastr.error('Došlo je do greške prilikom plaćanja.');
      console.log(error);
      throw error;
    }
  }

  provjeriUnosPodatakaKartice(): boolean {
    const podaciPlacanjaZaNarudzbu = this.blagajnaForm.get('placanjeForm')?.value;
  
    if (!podaciPlacanjaZaNarudzbu.vlasnikKartice || !podaciPlacanjaZaNarudzbu.brojKartice || !podaciPlacanjaZaNarudzbu.datumIsteka || !podaciPlacanjaZaNarudzbu.cvv) {
      this.handleError('Podaci plaćanja nisu uneseni');
      return false;
    }
  
    this.placanjeData = {
      vlasnikKartice: podaciPlacanjaZaNarudzbu.vlasnikKartice,
      brojKartice: podaciPlacanjaZaNarudzbu.brojKartice,
      datumIsteka: podaciPlacanjaZaNarudzbu.datumIsteka,
      cvv: podaciPlacanjaZaNarudzbu.cvv,
    };
    
    return true;
  }
  
  async provjeriPodatkeSaPlatnimSustavom() {
    try {
      const placanje: IPlacanje | null = await this.blagajnaService.provjeriPodatkePlacanja(this.placanjeData).toPromise();

      if (placanje) {
        this.preduvjetiZaNarudzbu = true;
      } else {
        this.preduvjetiZaNarudzbu = false;
      }
    } catch (error) {
      this.handleError('Greška prilikom provjere podataka plaćanja', error);
      this.preduvjetiZaNarudzbu = false;
    }
  }

  private handleError(message: string, error?: any): void {
    this.toastr.error(message);
    if (error) console.error(error);
  }
  
}


