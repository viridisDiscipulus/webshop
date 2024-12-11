import { TestBed } from '@angular/core/testing';

import { NarudzbeService } from './narudzbe.service';

describe('NarudzbeService', () => {
  let service: NarudzbeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NarudzbeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
