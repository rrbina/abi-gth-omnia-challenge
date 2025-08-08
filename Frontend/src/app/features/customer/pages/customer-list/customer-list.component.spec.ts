import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CustomerListComponent } from './customer-list.component';
import { CustomerService } from '../../../../core/services/customer.service';
import { of, throwError } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { Component } from '@angular/core';
import { By } from '@angular/platform-browser';
import { MongoMessage } from '../../../../core/models/mongo.model';

@Component({ template: '' })
class DummyComponent {}

describe('CustomerListComponent', () => {
  let component: CustomerListComponent;
  let fixture: ComponentFixture<CustomerListComponent>;
  let customerServiceSpy: jasmine.SpyObj<CustomerService>;

  const customers = [
    { id: '1', customerName: 'John Doe' },
    { id: '2', customerName: 'Jane Smith' }
  ];

  const mockMongoMessage: MongoMessage = {
    id: 'mock-id',
    message: JSON.stringify(customers),
    timestamp: new Date().toISOString()
  };

  beforeEach(waitForAsync(() => {
    const spy = jasmine.createSpyObj('CustomerService', ['getAll', 'delete']);

    TestBed.configureTestingModule({
      imports: [
        CustomerListComponent, // standalone
        RouterTestingModule.withRoutes([
          { path: 'customer/edit/:id', component: DummyComponent },
          { path: 'customer/new', component: DummyComponent }
        ])
      ],
      providers: [{ provide: CustomerService, useValue: spy }]
    }).compileComponents();

    customerServiceSpy = TestBed.inject(CustomerService) as jasmine.SpyObj<CustomerService>;
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load customers on init', () => {
    customerServiceSpy.getAll.and.returnValue(of(customers));
    fixture.detectChanges();

    expect(customerServiceSpy.getAll).toHaveBeenCalled();
    expect(component.customers.length).toBe(2);
  });

  it('should handle loading state', () => {
    customerServiceSpy.getAll.and.returnValue(of(customers));
    fixture.detectChanges();

    const loadingEl = fixture.debugElement.query(By.css('.alert-info'));
    expect(loadingEl).toBeNull();
  });

  it('should handle empty customer list', () => {
    const emptyMessage: MongoMessage = {
      id: 'empty-id',
      message: JSON.stringify([]),
      timestamp: new Date().toISOString()
    };

    customerServiceSpy.getAll.and.returnValue(of([]));
    fixture.detectChanges();

    const emptyEl = fixture.debugElement.query(By.css('.alert-warning'));
    expect(emptyEl).not.toBeNull();
  });

  it('should delete a customer', () => {
    customerServiceSpy.getAll.and.returnValue(of(customers));
    customerServiceSpy.delete.and.returnValue(of(null));
    fixture.detectChanges();

    component.deleteCustomer('1');

    expect(customerServiceSpy.delete).toHaveBeenCalledWith('1');
    expect(component.customers.length).toBe(1);
  });
});