import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  private message: string | null = null;

  setMessage(message: string) {
    this.message = message;
  }

  getMessage(): string | null {
    const currentMessage = this.message;
    this.message = null;
    return currentMessage;
  }
}