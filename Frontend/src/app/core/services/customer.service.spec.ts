import { TestBed } from '@angular/core/testing';
import { CustomerService } from './customer.service';
import { ApiRoutes } from '../constants/api-routes';
import { environment } from '../../../environments/environment';
import { CreateCustomer, Customer, UpdateCustomer } from '../models/customer.model';
import { PollingService } from './polling.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { MongoMessage } from '../models/mongo.model';

describe('CustomerService', () => {
  let service: CustomerService;
  let pollingServiceSpy: jasmine.SpyObj<PollingService>;
  let httpMock: HttpTestingController;
  const baseUrl = `${environment.producerApiUrl}/${ApiRoutes.Customer}`;

  const mockMongoMessage: MongoMessage = {
    id: 'event-id',
    message: 'operation complete',
    timestamp: new Date().toISOString()
  };

  const mockCustomer: Customer = { id: '123', customerName: 'Customer A' };
  const mockCustomers: Customer[] = [
    { id: '1', customerName: 'Customer A' },
    { id: '2', customerName: 'Customer B' }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('PollingService', ['pollEvent']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        CustomerService,
        { provide: PollingService, useValue: spy }
      ]
    });

    service = TestBed.inject(CustomerService);
    pollingServiceSpy = TestBed.inject(PollingService) as jasmine.SpyObj<PollingService>;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve all customers (getAll)', () => {
    service.getAll().subscribe(result => {
      expect(result).toEqual(mockCustomers);
    });

    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockCustomers);
  });

  it('should retrieve a customer by ID (getById)', () => {
    service.getById('123').subscribe(result => {
      expect(result).toEqual(mockCustomer);
    });

    const req = httpMock.expectOne(`${baseUrl}/123`);
    expect(req.request.method).toBe('GET');
    req.flush(mockCustomer);
  });

  it('should create a customer (create)', () => {
    const newCustomer: CreateCustomer = { customerName: 'Customer D' };

    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.create(newCustomer).subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(baseUrl, 'POST', newCustomer, 3);
  });

  it('should update a customer (update)', () => {
    const updatedCustomer: UpdateCustomer = { id: '456', customerName: 'Updated Name' };

    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.update(updatedCustomer.id, updatedCustomer).subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/456`, 'PUT', updatedCustomer, 3);
  });

  it('should delete a customer (delete)', () => {
    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.delete('789').subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/789`, 'DELETE', null, 3);
  });
});