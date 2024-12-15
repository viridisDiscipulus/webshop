import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IKosarica } from 'src/app/shared/models/kosarica';

@Component({
  selector: 'app-blagajna-pregled',
  templateUrl: './blagajna-pregled.component.html',
  styleUrls: ['./blagajna-pregled.component.scss']
})
export class BlagajnaPregledComponent implements OnInit {
  @Input() appStepper: CdkStepper;
  kosarica$: Observable<IKosarica>;

  constructor(private kosaricaService: KosaricaService, private toastr: ToastrService) { }

  ngOnInit() {
    this.kosarica$ = this.kosaricaService.kosarica$;
  }


}
