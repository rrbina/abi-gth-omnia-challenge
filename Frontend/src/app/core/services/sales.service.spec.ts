import { TestBed } from '@angular/core/testing';
import { SalesService } from './sales.service';
import { ApiRoutes } from '../constants/api-routes';
import { environment } from '../../../environments/environment';
import { CreateSale, Sale, UpdateSale } from '../models/sale.model';
import { PollingService } from './polling.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { MongoMessage } from '../models/mongo.model';

describe('SalesService', () => {
  let service: SalesService;
  let pollingServiceSpy: jasmine.SpyObj<PollingService>;
  let httpMock: HttpTestingController;
  const baseUrl = `${environment.producerApiUrl}/${ApiRoutes.Sales}`;

  const mockMongoMessage: MongoMessage = {
    id: 'event-id',
    message: 'operation complete',
    timestamp: new Date().toISOString()
  };

  const mockSale: Sale = {
    saleNumber: 'S002',
    saleDate: '2025-08-03',
    customerId: '3',
    customerName: 'Customer C',
    branchName: 'Branch Z',
    items: [],
    totalAmount: 300,
    isCancelled: false
  };

  const mockSales: Sale[] = [mockSale];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('PollingService', ['pollEvent']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        SalesService,
        { provide: PollingService, useValue: spy }
      ]
    });

    service = TestBed.inject(SalesService);
    pollingServiceSpy = TestBed.inject(PollingService) as jasmine.SpyObj<PollingService>;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve all sales (getAll)', () => {
    service.getAll().subscribe(result => {
      expect(result).toEqual(mockSales);
    });

    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockSales);
  });

  it('should retrieve a sale by ID (getById)', () => {
    service.getById('S002').subscribe(result => {
      expect(result).toEqual(mockSale);
    });

    const req = httpMock.expectOne(`${baseUrl}/S002`);
    expect(req.request.method).toBe('GET');
    req.flush(mockSale);
  });

  it('should create a sale (create)', () => {
    const newSale: CreateSale = {
      saleDto: {
        saleDate: '2025-08-03',
        customerId: '3',
        customerName: 'Customer C',
        branchName: 'Branch Z',
        totalAmount: 300,
        items: [],
        isCancelled: false
      }
    };

    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.create(newSale).subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(baseUrl, 'POST', newSale, 3);
  });

  it('should update a sale (update)', () => {
    const updateSale: UpdateSale = {
      saleNumber: 'S004',
      saleDate: '2025-08-04',
      customerId: '4',
      customerName: 'Customer D',
      branchId: 'B001',
      branchName: 'Branch D',
      items: []
    };

    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.update(updateSale.saleNumber, updateSale).subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/S004`, 'PUT', updateSale, 3);
  });

  it('should delete a sale (delete)', () => {
    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.delete('S005').subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/S005`, 'DELETE', null, 3);
  });

  it('should cancel a sale (cancelSale)', () => {
    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.cancelSale('S006').subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/S006/cancel`, 'PATCH', {}, 3);
  });

  it('should cancel a sale item (cancelItem)', () => {
    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.cancelItem('S007', 'ITEM001').subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/S007/items/ITEM001/cancel`, 'PATCH', {}, 3);
  });
});