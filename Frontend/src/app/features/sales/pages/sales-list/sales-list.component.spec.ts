import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { SalesListComponent } from './sales-list.component';
import { SalesService } from '../../../../core/services/sales.service';
import { of, throwError } from 'rxjs';
import { Sale } from '../../../../core/models/sale.model';
import { MongoMessage } from '../../../../core/models/mongo.model';
import { By } from '@angular/platform-browser';
import { delay } from 'rxjs/operators';

describe('SalesListComponent', () => {
  let component: SalesListComponent;
  let fixture: ComponentFixture<SalesListComponent>;
  let salesServiceSpy: jasmine.SpyObj<SalesService>;

  const mockSales: Sale[] = [
    {
      saleNumber: '1',
      saleDate: '2025-08-05T00:00:00',
      customerId: 'c1',
      customerName: 'Cliente A',
      branchName: 'Filial 1',
      totalAmount: 100.0,
      items: [],
      discount: 10.0,
      isCancelled: false
    },
    {
      saleNumber: '2',
      saleDate: '2025-08-04T00:00:00',
      customerId: 'c2',
      customerName: 'Cliente B',
      branchName: 'Filial 2',
      totalAmount: 200.0,
      items: [],
      discount: 20.0,
      isCancelled: true
    }
  ];

  const mockMongoMessage: MongoMessage = {
    id: '123',
    message: JSON.stringify(mockSales),
    timestamp: new Date().toISOString()
  };

  beforeEach(waitForAsync(() => {
    const spy = jasmine.createSpyObj('SalesService', ['getAll', 'cancelSale', 'delete']);

    TestBed.configureTestingModule({
      imports: [SalesListComponent],
      providers: [{ provide: SalesService, useValue: spy }]
    }).compileComponents();

    salesServiceSpy = TestBed.inject(SalesService) as jasmine.SpyObj<SalesService>;
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch and render sales on init', () => {
    salesServiceSpy.getAll.and.returnValue(of(mockSales));
    fixture.detectChanges();

    expect(component.sales.length).toBe(2);

    const rows = fixture.debugElement.queryAll(By.css('tbody tr'));
    expect(rows.length).toBe(2);

    const firstRowText = rows[0].nativeElement.textContent;
    expect(firstRowText).toContain('Cliente A');
    expect(firstRowText).toContain('Filial 1');
  });

  it('should show loading state before data is fetched', () => {
    const delayedMessage: MongoMessage = {
      id: 'delayed',
      message: JSON.stringify([]),
      timestamp: new Date().toISOString()
    };

    salesServiceSpy.getAll.and.returnValue(of([]).pipe(delay(10)));

    fixture.detectChanges();

    const loadingMessage = fixture.debugElement.query(By.css('.alert-info'));
    expect(loadingMessage).not.toBeNull();
    expect(loadingMessage?.nativeElement.textContent).toContain('Carregando vendas...');
  });

  it('should show empty message when no sales', () => {
    const emptyMessage: MongoMessage = {
      id: 'empty',
      message: JSON.stringify([]),
      timestamp: new Date().toISOString()
    };

    salesServiceSpy.getAll.and.returnValue(of([]));
    fixture.detectChanges();

    const emptyEl = fixture.debugElement.query(By.css('.alert-warning'));
    expect(emptyEl).toBeTruthy();
    expect(emptyEl.nativeElement.textContent).toContain('Nenhuma venda encontrada');
  });

  it('should cancel a sale and mark as cancelled', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    salesServiceSpy.getAll.and.returnValue(of(mockSales));
    salesServiceSpy.cancelSale.and.returnValue(
      of({
        id: 'mock-id',
        message: '',
        timestamp: new Date().toISOString()
      } as MongoMessage)
    );

    fixture.detectChanges();
    component.cancelSale('1');

    expect(salesServiceSpy.cancelSale).toHaveBeenCalledWith('1');
    expect(component.sales[0].isCancelled).toBeTrue();
  });

  it('should not cancel sale if user declines confirmation', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    salesServiceSpy.getAll.and.returnValue(of(mockSales));

    fixture.detectChanges();
    component.cancelSale('1');

    expect(salesServiceSpy.cancelSale).not.toHaveBeenCalled();
  });

  it('should delete a sale from the list', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    salesServiceSpy.getAll.and.returnValue(of(mockSales));
    salesServiceSpy.delete.and.returnValue(
      of({
        id: 'mock-id',
        message: '',
        timestamp: new Date().toISOString()
      } as MongoMessage)
    );

    fixture.detectChanges();
    component.deleteSale('1');

    expect(salesServiceSpy.delete).toHaveBeenCalledWith('1');
    expect(component.sales.find(s => s.saleNumber === '1')).toBeUndefined();
  });

  it('should handle error when getAll fails', () => {
    salesServiceSpy.getAll.and.returnValue(throwError(() => new Error('Erro')));
    fixture.detectChanges();

    expect(component.loading).toBeFalse();
  });

  it('should disable cancel button if sale is already cancelled', () => {
    salesServiceSpy.getAll.and.returnValue(of(mockSales));
    fixture.detectChanges();

    const buttons = fixture.debugElement.queryAll(By.css('button'));
    const cancelBtn = buttons.find(btn =>
      btn.nativeElement.textContent.includes('Cancelar') &&
      btn.nativeElement.closest('tr')?.textContent.includes('Cliente B')
    );

    expect(cancelBtn?.nativeElement.disabled).toBeTrue();
  });
});