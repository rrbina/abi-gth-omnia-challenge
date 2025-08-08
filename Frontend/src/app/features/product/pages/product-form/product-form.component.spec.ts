import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductFormComponent } from './product-form.component';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { Product } from '../../../../core/models/product.model';
import { ProductService } from '../../../../core/services/product.service';
import { MongoMessage } from '../../../../core/models/mongo.model';

describe('ProductFormComponent', () => {
  let component: ProductFormComponent;
  let fixture: ComponentFixture<ProductFormComponent>;
  let productServiceSpy: jasmine.SpyObj<ProductService>;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockProduct: Product = {
    id: '1',
    productName: 'Test Product',
    unitPrice: 9.99
  };

  const mockMongoMessage: MongoMessage = {
    id: 'mock-id',
    message: JSON.stringify(mockProduct),
    timestamp: new Date().toISOString()
  };

  const setup = (routeId: string | null = null) => {
    const routeStub = {
      snapshot: {
        paramMap: {
          get: () => routeId
        }
      }
    };

    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      providers: [
        FormBuilder,
        { provide: ProductService, useValue: jasmine.createSpyObj('ProductService', ['getById', 'create', 'update']) },
        { provide: ActivatedRoute, useValue: routeStub },
        { provide: Router, useValue: jasmine.createSpyObj('Router', ['navigate']) }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ProductFormComponent);
    component = fixture.componentInstance;
    productServiceSpy = TestBed.inject(ProductService) as jasmine.SpyObj<ProductService>;
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  };

  it('should create component in create mode', () => {
    setup();
    fixture.detectChanges();
    expect(component).toBeTruthy();
    expect(component.isEditMode).toBeFalse();
  });

  it('should create component in edit mode and load data', () => {
    setup('1');
    productServiceSpy.getById.and.returnValue(of(mockProduct));
    fixture.detectChanges();

    expect(component.isEditMode).toBeTrue();
    expect(productServiceSpy.getById).toHaveBeenCalledWith('1');

    const formValue = component.form.value;
    expect(formValue.productName).toBe(mockProduct.productName);
    expect(formValue.unitPrice).toBe(mockProduct.unitPrice);
  });

  it('should handle error if product fails to load', () => {
    setup('1');
    productServiceSpy.getById.and.returnValue(throwError(() => new Error('Load error')));
    fixture.detectChanges();
    expect(component.form.value.productName).toBe('');
  });

  it('should not submit if form is invalid', () => {
    setup();
    fixture.detectChanges();
    component.form.setValue({ productName: '', unitPrice: 0 });
    component.onSubmit();
    expect(productServiceSpy.create).not.toHaveBeenCalled();
    expect(productServiceSpy.update).not.toHaveBeenCalled();
  });

  it('should call create on valid form in create mode', () => {
    setup();
    const createResponse: MongoMessage = {
      id: 'msg-id',
      message: JSON.stringify('new-id'),
      timestamp: new Date().toISOString()
    };

    productServiceSpy.create.and.returnValue(of(createResponse));
    fixture.detectChanges();
    component.form.setValue({ productName: 'New Product', unitPrice: 5 });
    component.onSubmit();
    expect(productServiceSpy.create).toHaveBeenCalledWith({
      productName: 'New Product',
      unitPrice: 5
    });
  });

  it('should handle error on create', () => {
    setup();
    productServiceSpy.create.and.returnValue(throwError(() => new Error('Create failed')));
    fixture.detectChanges();
    component.form.setValue({ productName: 'Fail Product', unitPrice: 5 });
    component.onSubmit();
    expect(productServiceSpy.create).toHaveBeenCalled();
  });

  it('should handle error on update', () => {
    setup('1');
    productServiceSpy.getById.and.returnValue(of(mockProduct));
    productServiceSpy.update.and.returnValue(throwError(() => new Error('Update failed')));
    fixture.detectChanges();
    component.form.setValue({ productName: 'Fail Update', unitPrice: 10 });
    component.onSubmit();
    expect(productServiceSpy.update).toHaveBeenCalled();
  });
});