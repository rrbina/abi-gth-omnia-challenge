import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../../../core/services/customer.service';
import { ProductService } from '../../../../core/services/product.service';
import { SalesService } from '../../../../core/services/sales.service';
import { Customer } from '../../../../core/models/customer.model';
import { Product } from '../../../../core/models/product.model';
import { SaleItem, CreateSale } from '../../../../core/models/sale.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalResultComponent } from '../../../shared/components/modal-result/modal-result.component';

@Component({
  selector: 'app-sales-form',
  templateUrl: './sales-form.component.html',
  styleUrls: ['./sales-form.component.css'],
  imports: [CommonModule, ReactiveFormsModule, ModalResultComponent]
})
export class SalesFormComponent implements OnInit {
  form: FormGroup;
  customers: Customer[] = [];
  products: Product[] = [];
  items: SaleItem[] = [];

  @ViewChild(ModalResultComponent)
  modal!: ModalResultComponent;
  
  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private productService: ProductService,
    private salesService: SalesService
  ) {
    this.form = this.fb.group({
      saleDate: [new Date().toISOString().substring(0, 10), Validators.required],
      customerId: ['', Validators.required],
      customerName: [{ value: '', disabled: true }],
      branchName: ['', Validators.required],
      productId: [''],
      quantity: ['']
    });
  }

  ngOnInit(): void {
    this.customerService.getAll().subscribe(result => {
      const customers = result;
      if (customers) this.customers = customers;
    });

    this.productService.getAll().subscribe(result => {
      const products = result;
      if (products) this.products = products;
    });

    this.form.get('customerId')?.valueChanges.subscribe(id => {
      const customer = this.customers.find(c => c.id === id);
      this.form.patchValue({ customerName: customer?.customerName || '' });
    });
  }

  ShowMessage():void {    
    this.modal.show("Operação Realizada Com Sucesso");
  }

  addItem(): void {
    const productId = this.form.get('productId')?.value;
    const quantity = Number(this.form.get('quantity')?.value);
    if (!productId || !quantity || quantity < 1) return;

    const product = this.products.find(p => p.id === productId);
    if (!product || quantity > 20) return;

    let discount = 0;
    if (quantity >= 4 && quantity < 10) discount = 0.1;
    else if (quantity >= 10 && quantity <= 20) discount = 0.2;

    const totalDiscount = product.unitPrice * quantity * discount;

    this.items.push({
      id: crypto.randomUUID(),
      productId: product.id,
      unitPrice: product.unitPrice,
      quantity,
      discount: totalDiscount,
      isCancelled: false
    });

    this.form.patchValue({ productId: '', quantity: '' });
  }

  removeItem(id: string): void {
    this.items = this.items.filter(i => i.id !== id);
  }

  get totalAmount(): number {
    return this.items.reduce((sum, i) => sum + (i.unitPrice * i.quantity - i.discount), 0);
  }

  get totalDiscount(): number {
    return this.items.reduce((sum, i) => sum + i.discount, 0);
  }

  submit(): void {
  if (this.form.invalid || this.items.length === 0) return;

  const payload: CreateSale = {
      saleDto: {
        saleDate: this.form.get('saleDate')?.value,
        customerId: this.form.get('customerId')?.value,
        customerName: this.form.get('customerName')?.value,
        branchName: this.form.get('branchName')?.value,
        totalAmount: this.totalAmount,
        items: this.items,
        isCancelled: false
      }
    };

    this.salesService.create(payload).subscribe({
      next: (result) => 
      {
        this.ShowMessage();
      }
    });
  }

}