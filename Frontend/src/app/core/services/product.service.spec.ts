import { TestBed } from '@angular/core/testing';
import { ProductService } from './product.service';
import { ApiRoutes } from '../constants/api-routes';
import { environment } from '../../../environments/environment';
import { CreateProduct, Product, UpdateProduct } from '../models/product.model';
import { PollingService } from './polling.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { MongoMessage } from '../models/mongo.model';

describe('ProductService', () => {
  let service: ProductService;
  let pollingServiceSpy: jasmine.SpyObj<PollingService>;
  let httpMock: HttpTestingController;
  const baseUrl = `${environment.producerApiUrl}/${ApiRoutes.Product}`;

  const mockMongoMessage: MongoMessage = {
    id: 'event-id',
    message: 'operation complete',
    timestamp: new Date().toISOString()
  };

  const mockProduct: Product = { id: '123', productName: 'Product A', unitPrice: 10 };
  const mockProducts: Product[] = [
    { id: '1', productName: 'Product A', unitPrice: 10 },
    { id: '2', productName: 'Product B', unitPrice: 20 }
  ];

  beforeEach(() => {
    const spy = jasmine.createSpyObj('PollingService', ['pollEvent']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        ProductService,
        { provide: PollingService, useValue: spy }
      ]
    });

    service = TestBed.inject(ProductService);
    pollingServiceSpy = TestBed.inject(PollingService) as jasmine.SpyObj<PollingService>;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve all products (getAll)', () => {
    service.getAll().subscribe(result => {
      expect(result).toEqual(mockProducts);
    });

    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockProducts);
  });

  it('should retrieve a product by ID (getById)', () => {
    service.getById('123').subscribe(result => {
      expect(result).toEqual(mockProduct);
    });

    const req = httpMock.expectOne(`${baseUrl}/123`);
    expect(req.request.method).toBe('GET');
    req.flush(mockProduct);
  });

  it('should create a product (create)', () => {
    const newProduct: CreateProduct = { productName: 'Product D', unitPrice: 40 };

    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.create(newProduct).subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(baseUrl, 'POST', newProduct, 3);
  });

  it('should update a product (update)', () => {
    const updatedProduct: UpdateProduct = { id: '456', productName: 'Updated Product', unitPrice: 50 };

    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.update(updatedProduct.id, updatedProduct).subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/456`, 'PUT', updatedProduct, 3);
  });

  it('should delete a product (delete)', () => {
    pollingServiceSpy.pollEvent.and.returnValue(of(mockMongoMessage));

    service.delete('789').subscribe(result => {
      expect(result).toEqual(mockMongoMessage);
    });

    expect(pollingServiceSpy.pollEvent).toHaveBeenCalledWith(`${baseUrl}/789`, 'DELETE', null, 3);
  });
});