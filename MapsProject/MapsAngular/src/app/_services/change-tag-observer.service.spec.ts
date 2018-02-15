import { TestBed, inject } from '@angular/core/testing';

import { ChangeCoordObserverService } from './change-coord-observer.service';

describe('ChangeCoordObserverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChangeCoordObserverService]
    });
  });

  it('should be created', inject([ChangeCoordObserverService], (service: ChangeCoordObserverService) => {
    expect(service).toBeTruthy();
  }));
});
