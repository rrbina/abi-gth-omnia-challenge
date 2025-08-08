import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiRoutes } from '../constants/api-routes';
import { CreateCustomer, Customer, UpdateCustomer } from '../models/customer.model';
import { environment } from '../../../environments/environment';
import { PollingService } from './polling.service';
import { MongoMessage } from '../models/mongo.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private readonly baseUrl = `${environment.producerApiUrl}/${ApiRoutes.Customer}`;

  constructor(private http: HttpClient, 
    private pollingService: PollingService) {}

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseUrl);
  }

  getById(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseUrl}/${id}`);
  }

create(customer: CreateCustomer): Observable<MongoMessage | null> {
  return this.pollingService.pollEvent(
    this.baseUrl,
    'POST',
    customer,
    3
  );
}


  update(id: string, customer: UpdateCustomer): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      `${this.baseUrl}/${id}`,
      'PUT',
      customer,
      3
    );
  }

  delete(id: string): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      `${this.baseUrl}/${id}`,
      'DELETE',
      null,
      3
    );
  }
}