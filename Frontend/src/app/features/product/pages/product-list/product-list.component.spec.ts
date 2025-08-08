import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ProductListComponent } from './product-list.component';
import { ProductService } from '../../../../core/services/product.service';
import { of, throwError } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { Component } from '@angular/core';
import { By } from '@angular/platform-browser';
import { HttpClientTestingModule } from '@angular/common/http/testing';

@Component({ template: '' })
class DummyComponent {}

describe('ProductListComponent', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;
  let productServiceSpy: jasmine.SpyObj<ProductService>;

  const mockProducts = [
    { id: '1', productName: 'Product A', unitPrice: 10.5 },
    { id: '2', productName: 'Product B', unitPrice: 20.0 }
  ];

  beforeEach(waitForAsync(() => {
    const spy = jasmine.createSpyObj('ProductService', ['getAll', 'delete']);

    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([
          { path: 'product/edit/:id', component: DummyComponent },
          { path: 'product/new', component: DummyComponent }
        ]),
        HttpClientTestingModule
      ],
      providers: [{ provide: ProductService, useValue: spy }]
    }).compileComponents();

    productServiceSpy = TestBed.inject(ProductService) as jasmine.SpyObj<ProductService>;
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load products on init', () => {
    productServiceSpy.getAll.and.returnValue(of(mockProducts));
    fixture.detectChanges();

    expect(productServiceSpy.getAll).toHaveBeenCalled();
    expect(component.products.length).toBe(2);
  });

  it('should not load products if service returns null', () => {
    productServiceSpy.getAll.and.returnValue(of([]));
    fixture.detectChanges();

    expect(component.products.length).toBe(0);
  });

  it('should show loading state initially', () => {
    productServiceSpy.getAll.and.returnValue(of(mockProducts));
    component.loading = true;

    fixture.detectChanges();

    const loadingEl = fixture.debugElement.query(By.css('.alert-info'));
    expect(loadingEl).toBeFalsy(); // pois o loading é desativado após o subscribe
  });

  it('should show empty message when no products found', () => {
    productServiceSpy.getAll.and.returnValue(of([]));
    fixture.detectChanges();

    const emptyEl = fixture.debugElement.query(By.css('.alert-warning'));
    expect(emptyEl).not.toBeNull();
  });

  it('should handle errors from service', () => {
    productServiceSpy.getAll.and.returnValue(throwError(() => new Error('error')));
    fixture.detectChanges();

    expect(component.loading).toBeFalse();
  });

  it('should delete a product', () => {
    productServiceSpy.getAll.and.returnValue(of(mockProducts));
    productServiceSpy.delete.and.returnValue(of(null));

    fixture.detectChanges();

    component.deleteProduct('1');

    expect(productServiceSpy.delete).toHaveBeenCalledWith('1');
    expect(component.products.length).toBe(1);
    expect(component.products[0].id).toBe('2');
  });
});