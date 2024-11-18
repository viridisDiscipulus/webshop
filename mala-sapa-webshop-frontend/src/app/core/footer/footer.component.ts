import { Component, OnInit } from '@angular/core';
import { FooterVisibilityService } from '../services/footer-visibility.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
  vidljivostModula = false;

  constructor(private footerVisibilityService: FooterVisibilityService) { }

  ngOnInit(): void {
  //Subscribe na stanje vidljivosti
    this.footerVisibilityService.vidljivost$.subscribe(vidljivost => {
      this.vidljivostModula = vidljivost;
    });
  }

}
