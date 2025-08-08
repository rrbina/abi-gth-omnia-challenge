import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
declare var $: any;

@Component({
  selector: 'app-modal-result',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './modal-result.component.html',
  styleUrls: ['./modal-result.component.css']
})
export class ModalResultComponent {
  message: string | null = null;
  
  show(message: string): void {
    this.message = message;
    ($('#resultModal') as any).modal('show');
  }

  hide(): void {
    ($('#resultModal') as any).modal('hide');
  }
}