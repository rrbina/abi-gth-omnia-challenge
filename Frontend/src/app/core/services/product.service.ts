import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiRoutes } from '../constants/api-routes';
import { CreateProduct, Product, UpdateProduct } from '../models/product.model';
import { environment } from '../../../environments/environment';
import { PollingService } from './polling.service';
import { MongoMessage } from '../models/mongo.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private readonly baseUrl = `${environment.producerApiUrl}/${ApiRoutes.Product}`;

  constructor(private http: HttpClient, private pollingService: PollingService) {}

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  getById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }


  create(product: CreateProduct): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      this.baseUrl,
      'POST',
      product,
      3
    );
  }

  update(id: string, product: UpdateProduct): Observable<MongoMessage | null> {
    return this.pollingService.pollEvent(
      `${this.baseUrl}/${id}`,
      'PUT',
      product,
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