import { TestBed } from '@angular/core/testing';

import { UsersApiCallsService } from './users-api-calls.service';

describe('UserApiCallsService', () => {
  let service: UsersApiCallsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UsersApiCallsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
