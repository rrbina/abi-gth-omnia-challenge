import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { ApiRoutes } from '../constants/api-routes';
import { CreateSale, Sale, UpdateSale } from '../models/sale.model';
import { PollingService } from './polling.service';
import { MongoMessage } from '../models/mongo.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private readonly baseUrl = `${environment.producerApiUrl}/${ApiRoutes.Sales}`;

  constructor(private http: HttpClient, private pollingService: PollingService) {}

  getAll(): Observable<Sale[]> {
    return this.http.get<Sale[]>(this.baseUrl);
  }

  getById(id: string): Observable<Sale> {
    return this.http.get<Sale>(`${this.baseUrl}/${id}`);
  }

  create(sale: CreateSale): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      this.baseUrl,
      'POST',
      sale,
      3
    );
  }

  update(id: string, sale: UpdateSale): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      `${this.baseUrl}/${id}`,
      'PUT',
      sale,
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

  cancelSale(saleId: string): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      `${this.baseUrl}/${saleId}/cancel`,
      'PATCH',
      {},
      3
    );
  }

  cancelItem(saleId: string, itemId: string): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      `${this.baseUrl}/${saleId}/items/${itemId}/cancel`,
      'PATCH',
      {},
      3
    );
  }
}