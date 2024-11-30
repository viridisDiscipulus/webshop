import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { KorisnickiRacunService } from '../korisnicki-racun.service';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.scss']
})
export class RegistracijaComponent implements OnInit {

  registracijaForma: FormGroup;
  errors: string[];

  constructor(private fb: FormBuilder, private korisnickiRacunService: KorisnickiRacunService, private router: Router) { }

  ngOnInit(): void {
    this.kreirajRegistracijaFormu();
  }

   kreirajRegistracijaFormu() {
    this.registracijaForma = this.fb.group({
      alias: [null, [Validators.required]],
      email: [null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validirajDostupnostEmaila()]
      ],
      lozinka: [null, [Validators.required]]
    });
   }

   registrirajSe() {
    this.korisnickiRacunService.registracija(this.registracijaForma.value).subscribe(data => {
      this.router.navigateByUrl('/webshop');
    }, error => {
      //console.log(error);
      this.errors = error.errors;
    });  
  }

  validirajDostupnostEmaila(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.korisnickiRacunService.proveriEmail(control.value).pipe(
            map(res => {
              return res ? { emailPostoji: true } : null;
            })
          );
        })
      );
    };
  }

}
