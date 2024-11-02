import { Component } from '@angular/core';
import { PaymentMethod } from '../../models/payment-method';
import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-static-material-table',
  templateUrl: './static-material-table.component.html',
  styleUrl: './static-material-table.component.css'
})
export class StaticMaterialTableComponent {

  constructor() { }
  // TODO: replace this with real data from your application
  data: PaymentMethod[] = [{ name: 'Axis Rewards', billingDate: new Date('18-09-2024'), status: true },
  { name: 'HPCL', billingDate: new Date('20-09-2024'), status: true },
  { name: 'BPCL', billingDate: new Date('04-09-2024'), status: true },
  { name: 'AMAZON ICICI', billingDate: new Date('14-09-2024'), status: true },
  { name: 'Axis Neo', billingDate: new Date('12-09-2024'), status: true }
  ];
}
