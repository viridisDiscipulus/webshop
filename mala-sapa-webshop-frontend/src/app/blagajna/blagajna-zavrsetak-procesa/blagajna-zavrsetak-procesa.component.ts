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

    if (state && this.isINarudzba(state)) {
      this.narudzba = {
        id: state.id,
        kupacEmail: state.kupacEmail,
        datumNarudzbe: state.datumNarudzbe,
        adresaDostave: {
          ime: state.adresaDostave.ime,
          prezime: state.adresaDostave.prezime,
          ulica: state.adresaDostave.ulica,
          grad: state.adresaDostave.grad,
          drzava: state.adresaDostave.drzava,
          postanskiBroj: state.adresaDostave.postanskiBroj,
        },
        nacinIsporuke: state.nacinIsporuke,
        cijenaDostave: state.cijenaDostave,
        naruceniArtikli: state.naruceniArtikli.map((artikl: any) => ({
          proizvodId: artikl.proizvodId,
          proizvodNaziv: artikl.proizvodNaziv,
          slikaUrl: artikl.slikaUrl,
          cijena: artikl.cijena,
          kolicina: artikl.kolicina,
        })),
        ukupnaCijena: state.ukupnaCijena,
        status: state.status,
        sveukupnaCijena: state.sveukupnaCijena,
      } as INarudzba;
    }
    else {
      console.error('Nevažeći objekt', state);
    }
  }

  ngOnInit(): void {
  }

  isINarudzba(obj: any): obj is INarudzba {
    return (
      typeof obj.id === 'number' &&
      typeof obj.kupacEmail === 'string' &&
      typeof obj.datumNarudzbe === 'string' &&
      typeof obj.adresaDostave === 'object' &&
      typeof obj.nacinIsporuke === 'string' &&
      typeof obj.cijenaDostave === 'number' &&
      Array.isArray(obj.naruceniArtikli) &&
      typeof obj.status === 'string'
    );
  }
}


