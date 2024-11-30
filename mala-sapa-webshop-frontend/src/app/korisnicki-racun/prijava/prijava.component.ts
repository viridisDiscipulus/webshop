import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { KorisnickiRacunService } from '../korisnicki-racun.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-prijava',
  templateUrl: './prijava.component.html',
  styleUrls: ['./prijava.component.scss']
})
export class PrijavaComponent implements OnInit {
  prijavaForm: FormGroup;
  returnUrl: string;

  constructor(private korisnickiRacun: KorisnickiRacunService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/webshop';
    this.kreirajPrijavaFormu();
  }

  kreirajPrijavaFormu() {
    this.prijavaForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      lozinka: new FormControl('', [Validators.required])
    });
  }

  prijaviSe() {
    this.korisnickiRacun.prijava(this.prijavaForm.value).subscribe(() => {
      this.router.navigateByUrl(this.returnUrl);
      console.log('Uspjesno prijavljen');
    }, (error) => {
      console.error('Greska kod prijave', error);
    });
  } 

}
