import { Component, OnInit, ViewChild } from '@angular/core';
import { Product } from '../../../../core/models/product.model';
import { ProductService } from '../../../../core/services/product.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ModalResultComponent } from '../../../shared/components/modal-result/modal-result.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterModule, ModalResultComponent]
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = false;

  @ViewChild(ModalResultComponent)
  modal!: ModalResultComponent;
  
  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.fetchProducts();
  }

  fetchProducts(): void {
    this.loading = true;
    this.productService.getAll().subscribe({
      next: (result) => {
        const products = result;
        this.products = products ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  deleteProduct(id: string): void {
    this.productService.delete(id).subscribe({
      next: () => {
        this.products = this.products.filter(p => p.id !== id);
        this.ShowMessage();
      }
    });
  }

  ShowMessage():void {    
    this.modal.show("Operação Realizada Com Sucesso");
  }
}