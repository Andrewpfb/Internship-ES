import { TestBed, async, inject } from '@angular/core/testing';

import { LeaveModeratePageGuard } from './leave-moderate-page.guard';

describe('LeaveModeratePageGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LeaveModeratePageGuard]
    });
  });

  it('should ...', inject([LeaveModeratePageGuard], (guard: LeaveModeratePageGuard) => {
    expect(guard).toBeTruthy();
  }));
});
