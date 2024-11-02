import { Component } from '@angular/core';
import { PaymentMethod } from '../../models/payment-method';
import { Router } from '@angular/router';
import { MatCardCustom } from '../../models/mat-card';

@Component({
  selector: 'app-payment-method-manager',
  standalone: false,
  templateUrl: './payment-method-manager.component.html',
  styleUrl: './payment-method-manager.component.css'
})
export class PaymentMethodManagerComponent {
  paymentMethods: Array<PaymentMethod>;

  constructor(private router: Router) {
    this.paymentMethods = new Array<PaymentMethod>();
  }

  ngOnInit() {
    this.paymentMethods = [
      { name: 'Axis Rewards', billingDate: new Date('18-09-2024'), status: true },
      { name: 'HPCL', billingDate: new Date('20-09-2024'), status: true },
      { name: 'BPCL', billingDate: new Date('04-09-2024'), status: true },
      { name: 'AMAZON ICICI', billingDate: new Date('14-09-2024'), status: true },
      { name: 'Axis Neo', billingDate: new Date('12-09-2024'), status: true }
    ];
  }
}
