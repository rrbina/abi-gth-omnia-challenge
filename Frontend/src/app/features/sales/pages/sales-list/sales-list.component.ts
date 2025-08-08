import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { SalesService } from '../../../../core/services/sales.service';
import { Sale } from '../../../../core/models/sale.model';
import { ModalResultComponent } from '../../../shared/components/modal-result/modal-result.component';

@Component({
  selector: 'app-sales-list',
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ModalResultComponent]
})
export class SalesListComponent implements OnInit {
  sales: Sale[] = [];
  loading = false;
  
  @ViewChild(ModalResultComponent)
  modal!: ModalResultComponent;

  constructor(private salesService: SalesService) {}

  ngOnInit(): void {
    this.fetchSales();
  }

  fetchSales(): void {
    this.loading = true;
    this.salesService.getAll().subscribe({
     next: (data: Sale[] | null) => {
        this.sales = data ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  ShowMessage():void {    
    this.modal.show("Operação Realizada Com Sucesso");
  }

  cancelSale(id: string): void {
    if (confirm('Tem certeza que deseja cancelar esta venda?')) {
      this.salesService.cancelSale(id).subscribe(() => {
        const sale = this.sales.find(s => s.saleNumber === id);
        if (sale) {
          sale.isCancelled = true;
        }
        this.ShowMessage();
      });
    }
  }

  deleteSale(id: string): void {
    if (confirm('Tem certeza que deseja excluir esta venda?')) {
      this.salesService.delete(id).subscribe(() => {
        this.sales = this.sales.filter(s => s.saleNumber !== id);
        this.ShowMessage();
      });
    }
  }
}