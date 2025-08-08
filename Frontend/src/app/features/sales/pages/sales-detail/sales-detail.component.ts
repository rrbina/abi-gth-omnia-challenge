import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SalesService } from '../../../../core/services/sales.service';
import { Sale } from '../../../../core/models/sale.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ModalResultComponent } from '../../../shared/components/modal-result/modal-result.component';

@Component({
  selector: 'app-sales-detail',
  templateUrl: './sales-detail.component.html',
  styleUrls: ['./sales-detail.component.css'],  
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, ModalResultComponent]
})
export class SalesDetailComponent implements OnInit {
  sale: Sale = {
    saleDate: '',
    customerId: '',
    customerName: '',
    branchName: '',
    totalAmount: 0,
    items: [],
    isCancelled: false
  };
  
  @ViewChild(ModalResultComponent)
  modal!: ModalResultComponent;

  constructor(
    private salesService: SalesService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const saleId = this.route.snapshot.paramMap.get('id');
    if (saleId) {
      this.salesService.getById(saleId).subscribe({
        next: (result) => {
          const mapped = result;
          if (mapped) {
            this.sale = mapped;
          } else {
            this.resetSale();
          }
          this.ShowMessage();
        },
        error: () => {
          this.resetSale();
        }
      });
    }
  }

  ShowMessage():void {    
    this.modal.show("Operação Realizada Com Sucesso");
  }
  
  cancelSale(): void {
    if (!this.sale?.saleNumber) return;

    this.salesService.cancelSale(this.sale.saleNumber).subscribe({
      next: (result) => {
        if (result !== null) {
          this.sale.isCancelled = true;
        }
      },
      error: () => {}
    });
  }

  cancelItem(itemId: string): void {
    if (!this.sale?.saleNumber) return;

    this.salesService.cancelItem(this.sale.saleNumber, itemId).subscribe({
      next: (result) => {
        if (result !== null) {
          const item = this.sale?.items.find(i => i.id === itemId);
          if (item) item.isCancelled = true;
        }
      },
      error: () => {}
    });
  }

  isItemCancelled(itemId: string): boolean {
    return this.sale?.items.find(i => i.id === itemId)?.isCancelled ?? false;
  }

  private resetSale(): void {
    this.sale = {
      saleDate: '',
      customerId: '',
      customerName: '',
      branchName: '',
      totalAmount: 0,
      items: [],
      isCancelled: false
    };
  }
}