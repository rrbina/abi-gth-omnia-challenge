import { Injectable } from '@angular/core';
import { HttpClient, HttpContext } from '@angular/common/http';
import { interval, Observable, of, throwError } from 'rxjs';
import { switchMap, take, catchError, takeWhile, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { IGNORE_ERROR_INTERCEPTOR } from '../models/http-context-tokens';
import { MongoMessage } from '../models/mongo.model';

@Injectable({
  providedIn: 'root'
})
export class PollingService {
  private readonly confirmationBaseUrl = `${environment.consumerApiUrl}/confirmation`;

  constructor(private http: HttpClient) {}

  pollEvent(
    url: string,
    method: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH',
    body: any = null,
    maxRepetitions: number
  ): Observable<MongoMessage | null> {
    return this.sendInitialRequest(url, method, body).pipe(
      switchMap((eventId: any) => {
        
        const finalUrl = `${this.confirmationBaseUrl}`;
        console.log(eventId);
        return this.startConfirmationPolling(finalUrl, eventId?.eventId, maxRepetitions);
      })
    );
  }

  private sendInitialRequest(
    url: string,
    method: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH',
    body: any
  ): Observable<any> { // Alteração de 'string' para 'any'
    switch (method) {
      case 'GET':
        return this.http.get<any>(url);
      case 'POST':
        return this.http.post<any>(url, body);
      case 'PUT':
        return this.http.put<any>(url, body);
      case 'DELETE':
        return this.http.delete<any>(url);
      case 'PATCH':
        return this.http.patch<any>(url, body);
      default:
        return throwError(() => new Error('Método inválido para requisição inicial'));
    }
  }

  private startConfirmationPolling(
    confirmationUrl: any,
    eventId: any,
    maxRepetitions: number
  ): Observable<MongoMessage | null> {
    const context = new HttpContext().set(IGNORE_ERROR_INTERCEPTOR, true);

    return interval(2000).pipe(
      take(maxRepetitions),
      switchMap((): Observable<MongoMessage | null> =>
        this.http.post<any>(confirmationUrl, { id: eventId }, { context }).pipe(
          tap((response: any) => {
            if (response !== null) {
              console.log(`Requisição finalizada. Resultado: ${JSON.stringify(response)}`);
            }
          }),
          catchError(() => of(null)) // Caso de erro, retorna null
        )
      ),
      takeWhile((response: any) => {
        console.log(response)
        return response === null
      }, true)
    );
  }
}