import { Routes } from '@angular/router';

import { CustomerListComponent } from './features/customer/pages/customer-list/customer-list.component';
import { CustomerFormComponent } from './features/customer/pages/customer-form/customer-form.component';

import { ProductListComponent } from './features/product/pages/product-list/product-list.component';
import { ProductFormComponent } from './features/product/pages/product-form/product-form.component';

import { SalesListComponent } from './features/sales/pages/sales-list/sales-list.component';
import { SalesFormComponent } from './features/sales/pages/sales-form/sales-form.component';
import { SalesDetailComponent } from './features/sales/pages/sales-detail/sales-detail.component';
import { ErrorComponent } from './features/shared/error/error.component';


export const routes: Routes = [
  {
    path: '',
    redirectTo: 'customer',
    pathMatch: 'full',
  },
  {
    path: 'customer',
    children: [
      { path: '', component: CustomerListComponent },
      { path: 'new', component: CustomerFormComponent },
      { path: 'edit/:id', component: CustomerFormComponent },
    ]
  },
  {
    path: 'product',
    children: [
      { path: '', component: ProductListComponent },
      { path: 'new', component: ProductFormComponent },
      { path: 'edit/:id', component: ProductFormComponent },
    ]
  },
  {
    path: 'sales',
    children: [
      { path: '', component: SalesListComponent },
      { path: 'new', component: SalesFormComponent },
      { path: 'edit/:id', component: SalesFormComponent },
      { path: 'detail/:id', component: SalesDetailComponent },
    ]
  },
   { path: 'error', component: ErrorComponent }
];