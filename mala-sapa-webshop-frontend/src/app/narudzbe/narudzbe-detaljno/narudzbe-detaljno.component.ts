import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { INarudzba } from 'src/app/shared/models/narudzba';
import { BreadcrumbService } from 'xng-breadcrumb';
import { NarudzbeService } from '../narudzbe.service';

@Component({
  selector: 'app-narudzbe-detaljno',
  templateUrl: './narudzbe-detaljno.component.html',
  styleUrls: ['./narudzbe-detaljno.component.scss']
})
export class NarudzbeDetaljnoComponent implements OnInit {
  narudzba: INarudzba;

  constructor(private route: ActivatedRoute, private breadcrumbService: BreadcrumbService, private narudzbeService: NarudzbeService) 
  {
    this.breadcrumbService.set('@Narudžba detaljno', '');
   }

   ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id'); 
    this.narudzbeService.getNarudzba(id).subscribe(
      (narudzba: INarudzba) => {
        this.narudzba = narudzba;
        this.breadcrumbService.set('@Narudžba detaljno', `Narudzba #${narudzba.id} - ${narudzba.status}`);
      },
      (error) => {
        console.error('Error fetching narudzba:', error);
      }
    );
  }
  

}
