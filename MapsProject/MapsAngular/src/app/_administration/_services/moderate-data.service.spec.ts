import { TestBed, inject } from '@angular/core/testing';

import { ModerateDataService } from './moderate-data.service';

describe('ModerateDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ModerateDataService]
    });
  });

  it('should be created', inject([ModerateDataService], (service: ModerateDataService) => {
    expect(service).toBeTruthy();
  }));
});
