import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadService {
  loadCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  load() {
    this.loadCount++;
    this.spinnerService.show(undefined, {
      type: 'ball-spin-clockwise',
      bdColor: 'rgba(85, 64, 79, 0.7)',
      color: '#333333',
      size: 'large'
    });
  }

  blank() {
    this.loadCount--;
    if (this.loadCount <= 0) {
      this.loadCount = 0;
      this.spinnerService.hide();
    }
  }


}
