import { TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PollingService } from './polling.service';

describe('PollingService', () => {
  let service: PollingService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    service = TestBed.inject(PollingService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });  

  it('should propagate error if request fails before maxRepetitions', fakeAsync(() => {
    const url = '/api/my-endpoint/789-ghi';
    const maxReps = 3;

    let errorCaught = false;

    service.pollEvent(url, 'GET', null, maxReps).subscribe({
      error: () => (errorCaught = true)
    });

    tick(2000);
    const req = httpMock.expectOne(url);
    req.flush({ message: 'Erro do servidor' }, { status: 500, statusText: 'Server Error' });

    expect(errorCaught).toBeTrue();
  }));
});