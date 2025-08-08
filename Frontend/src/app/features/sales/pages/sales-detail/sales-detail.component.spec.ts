import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SalesDetailComponent } from './sales-detail.component';
import { ActivatedRoute } from '@angular/router';
import { of, throwError } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { Sale } from '../../../../core/models/sale.model';
import { SalesService } from '../../../../core/services/sales.service';

describe('SalesDetailComponent', () => {
  let component: SalesDetailComponent;
  let fixture: ComponentFixture<SalesDetailComponent>;
  let salesServiceSpy: jasmine.SpyObj<SalesService>;
  let activatedRouteStub: any;

  const mockSale: Sale = {
    saleNumber: '1',
    saleDate: '2025-08-01T00:00:00Z',
    customerId: 'c1',
    customerName: 'John Doe',
    branchName: 'Main Branch',
    totalAmount: 100,
    discount: 20,
    isCancelled: false,
    items: [
      {
        id: 'i1',
        productId: 'p1',
        unitPrice: 10,
        quantity: 10,
        discount: 20,
        isCancelled: false
      },
      {
        id: 'i2',
        productId: 'p2',
        unitPrice: 5,
        quantity: 2,
        discount: 0,
        isCancelled: true
      }
    ]
  };

  beforeEach(async () => {
    activatedRouteStub = {
      snapshot: {
        paramMap: {
          get: () => '1'
        }
      }
    };

    salesServiceSpy = jasmine.createSpyObj<SalesService>('SalesService', ['getById', 'cancelSale', 'cancelItem']);

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, SalesDetailComponent],
      providers: [
        { provide: SalesService, useValue: salesServiceSpy },
        { provide: ActivatedRoute, useValue: activatedRouteStub }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SalesDetailComponent);
    component = fixture.componentInstance;
  });

  it('should create component', () => {
    salesServiceSpy.getById.and.returnValue(of(mockSale));
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should load sale on init', () => {
    salesServiceSpy.getById.and.returnValue(of(mockSale));
    fixture.detectChanges();
    expect(salesServiceSpy.getById).toHaveBeenCalledWith('1');
    expect(component.sale).toEqual(mockSale);
  });

  it('should handle error on cancel item', () => {
    salesServiceSpy.getById.and.returnValue(of(mockSale));
    salesServiceSpy.cancelItem.and.returnValue(throwError(() => new Error('Error canceling item')));
    fixture.detectChanges();

    component.cancelItem('i1');
    expect(salesServiceSpy.cancelItem).toHaveBeenCalledWith('1', 'i1');
  });

  it('should handle error on cancel sale', () => {
    salesServiceSpy.getById.and.returnValue(of(mockSale));
    salesServiceSpy.cancelSale.and.returnValue(throwError(() => new Error('Error canceling sale')));
    fixture.detectChanges();

    component.cancelSale();
    expect(salesServiceSpy.cancelSale).toHaveBeenCalledWith('1');
  });

  it('should mark sale as cancelled when cancelSale returns non-null', () => {
    salesServiceSpy.getById.and.returnValue(of(mockSale));
    salesServiceSpy.cancelSale.and.returnValue(of({
      id: 'mock-id',
      message: 'ok',
      timestamp: new Date().toISOString()
    }));
    fixture.detectChanges();

    component.cancelSale();
    expect(component.sale.isCancelled).toBeTrue();
  });
  
  it('should mark item as cancelled when cancelItem returns non-null', () => {
    salesServiceSpy.getById.and.returnValue(of(mockSale));
    salesServiceSpy.cancelItem.and.returnValue(of({
      id: 'mock-id',
      message: 'ok',
      timestamp: new Date().toISOString()
    }));
    fixture.detectChanges();

    component.cancelItem('i1');
    const item = component.sale.items.find(i => i.id === 'i1');
    expect(item?.isCancelled).toBeTrue();
  });

  it('should return false if item not found in isItemCancelled', () => {
    component.sale = mockSale;
    const result = component.isItemCancelled('unknown');
    expect(result).toBeFalse();
  });
});