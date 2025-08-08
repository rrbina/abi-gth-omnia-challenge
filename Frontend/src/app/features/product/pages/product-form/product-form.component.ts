import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Product, UpdateProduct, CreateProduct } from '../../../../core/models/product.model';
import { ProductService } from '../../../../core/services/product.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalResultComponent } from '../../../shared/components/modal-result/modal-result.component';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css'],
  imports: [CommonModule, ReactiveFormsModule, ModalResultComponent]
})
export class ProductFormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  productId: string | null = null;

  @ViewChild(ModalResultComponent)
  modal!: ModalResultComponent;
  
  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.productId;

    this.form = this.fb.group({
      productName: ['', [Validators.required, Validators.minLength(3)]],
      unitPrice: [0, [Validators.required, Validators.min(0.01)]]
    });

    if (this.isEditMode && this.productId) {
      this.productService.getById(this.productId).subscribe({
        next: (result) => {
          const product = result;
          if (product) {
            this.form.patchValue({
              productName: product.productName,
              unitPrice: product.unitPrice
            });
          }
        },
        error: () => {}
      });     
    }
  }

  ShowMessage():void {    
    this.modal.show("Operação Realizada Com Sucesso");
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const { productName, unitPrice } = this.form.value;

    if (this.isEditMode && this.productId) {
      const product: UpdateProduct = {
        id: this.productId,
        productName,
        unitPrice
      };
      this.productService.update(this.productId, product).subscribe({
        next: () => this.ShowMessage(),
        error: () => {}
      });
    } else {
      const newProduct: CreateProduct = {
        productName,
        unitPrice
      };
      this.productService.create(newProduct).subscribe({
        next: () => this.ShowMessage(),
        error: () => {}
      });
    }
  }
}