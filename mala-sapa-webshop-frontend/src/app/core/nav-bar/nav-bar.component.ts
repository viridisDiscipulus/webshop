import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { KosaricaService } from 'src/app/kosarica/kosarica.service';
import { IKosarica } from 'src/app/shared/models/kosarica';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  kosarica$: Observable<IKosarica>;

  constructor(private kosaricaService: KosaricaService) { }

  ngOnInit(): void {
    this.kosarica$ = this.kosaricaService.kosarica$;
  }

}
