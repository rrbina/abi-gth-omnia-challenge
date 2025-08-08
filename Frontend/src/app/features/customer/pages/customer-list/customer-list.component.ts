import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from '../../../../core/models/customer.model';
import { CustomerService } from '../../../../core/services/customer.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms'
import { RouterModule } from '@angular/router';
import { ModalResultComponent } from '../../../shared/components/modal-result/modal-result.component';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterModule, ModalResultComponent]
})
export class CustomerListComponent implements OnInit {
  customers: Customer[] = [];
  loading = false;
  @ViewChild(ModalResultComponent)
  modal!: ModalResultComponent;
  

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.fetchCustomers();
  }

  ShowMessage():void {    
    this.modal.show("Operação Realizada Com Sucesso");
  }

  fetchCustomers(): void {
    this.loading = true;
    this.customerService.getAll().subscribe({
      next: (result) => {
        const customers = result;
        this.customers = customers ?? [];
        this.loading = false;
      }
    });
  }

  deleteCustomer(id: string): void {
    this.customerService.delete(id).subscribe({
      next: () => {
        this.customers = this.customers.filter(c => c.id !== id);
        this.ShowMessage();
      }
    });
  }
}