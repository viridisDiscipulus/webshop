import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FooterVisibilityService {
  // BehaviorSubject za pracenje stanja vidljivosti footera
  private vidljivostSub = new BehaviorSubject<boolean>(false);
  vidljivost$ = this.vidljivostSub.asObservable();

  // Metoda azuriranja stanja vidljivosti
  setVidljivost(vidljivost: boolean): void {
    this.vidljivostSub.next(vidljivost);
  }
}
