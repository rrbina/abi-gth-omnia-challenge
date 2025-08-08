import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer, UpdateCustomer, CreateCustomer } from '../../../../core/models/customer.model';
import { CustomerService } from '../../../../core/services/customer.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ModalResultComponent } from '../../../shared/components/modal-result/modal-result.component';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, ModalResultComponent]
})
export class CustomerFormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  customerId: string | null = null;
  
  @ViewChild(ModalResultComponent)
  modal!: ModalResultComponent;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.customerId;

    this.form = this.fb.group({
      customerName: ['', [Validators.required, Validators.minLength(3)]]
    });

    if (this.isEditMode && this.customerId) {
      this.customerService.getById(this.customerId).subscribe({
        next: (result) => {
          if (result) {
            this.form.patchValue({
              customerName: result.customerName
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

    const customerName = this.form.value.customerName;

    if (this.isEditMode && this.customerId) {
      const customer: UpdateCustomer = {
        id: this.customerId,
        customerName
      };
      this.customerService.update(this.customerId, customer).subscribe({
        next: (result) => {
          this.ShowMessage();
        },
        error: () => {}
      });
    } else {
      const newCustomer: CreateCustomer = { customerName };
      this.customerService.create(newCustomer).subscribe({
        next: (result) => {
          this.ShowMessage();
        },
        error: () => {}
      });
    }
  }
}