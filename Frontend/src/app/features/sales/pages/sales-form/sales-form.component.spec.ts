import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SalesFormComponent } from '../sales-form/sales-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomerService } from '../../../../core/services/customer.service';
import { ProductService } from '../../../../core/services/product.service';
import { SalesService } from '../../../../core/services/sales.service';
import { of } from 'rxjs';
import { Customer } from '../../../../core/models/customer.model';
import { Product } from '../../../../core/models/product.model';
import { MongoMessage } from '../../../../core/models/mongo.model';

describe('SalesFormComponent', () => {
  let component: SalesFormComponent;
  let fixture: ComponentFixture<SalesFormComponent>;
  let customerServiceSpy: jasmine.SpyObj<CustomerService>;
  let productServiceSpy: jasmine.SpyObj<ProductService>;
  let salesServiceSpy: jasmine.SpyObj<SalesService>;

  const mockCustomers: Customer[] = [
    { id: 'c1', customerName: 'Cliente 1' }
  ];

  const mockProducts: Product[] = [
    { id: 'p1', productName: 'Produto 1', unitPrice: 100 }
  ];

  const mockMongoCustomers: MongoMessage = {
  id: 'mongo-id-1',
  message: JSON.stringify(mockCustomers),
  timestamp: new Date().toISOString()
};

const mockMongoProducts: MongoMessage = {
  id: 'mongo-id-2',
  message: JSON.stringify(mockProducts),
  timestamp: new Date().toISOString()
};

  beforeEach(async () => {
    const customerSpy = jasmine.createSpyObj('CustomerService', ['getAll']);
    const productSpy = jasmine.createSpyObj('ProductService', ['getAll']);
    const salesSpy = jasmine.createSpyObj('SalesService', ['create']);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      providers: [
        { provide: CustomerService, useValue: customerSpy },
        { provide: ProductService, useValue: productSpy },
        { provide: SalesService, useValue: salesSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SalesFormComponent);
    component = fixture.componentInstance;

    customerServiceSpy = TestBed.inject(CustomerService) as jasmine.SpyObj<CustomerService>;
    productServiceSpy = TestBed.inject(ProductService) as jasmine.SpyObj<ProductService>;
    salesServiceSpy = TestBed.inject(SalesService) as jasmine.SpyObj<SalesService>;

    customerServiceSpy.getAll.and.returnValue(of(mockCustomers));
    productServiceSpy.getAll.and.returnValue(of(mockProducts));
  });

  it('should create', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should load customers and products on init', () => {
    fixture.detectChanges();
    expect(customerServiceSpy.getAll).toHaveBeenCalled();
    expect(productServiceSpy.getAll).toHaveBeenCalled();
    expect(component.customers.length).toBe(1);
    expect(component.products.length).toBe(1);
  });

  it('should add item with correct discount', () => {
    fixture.detectChanges();

    component.form.patchValue({ productId: 'p1', quantity: 10 });
    component.addItem();

    expect(component.items.length).toBe(1);
    const item = component.items[0];
    expect(item.discount).toBeCloseTo(100 * 10 * 0.2);
  });

  it('should not add item if quantity > 20', () => {
    fixture.detectChanges();

    component.form.patchValue({ productId: 'p1', quantity: 25 });
    component.addItem();

    expect(component.items.length).toBe(0);
  });

  it('should remove item', () => {
    fixture.detectChanges();

    component.form.patchValue({ productId: 'p1', quantity: 5 });
    component.addItem();

    const id = component.items[0].id;
    component.removeItem(id);

    expect(component.items.length).toBe(0);
  });  

  it('should not submit if form is invalid', () => {
    fixture.detectChanges();
    component.submit();
    expect(salesServiceSpy.create).not.toHaveBeenCalled();
  });
});