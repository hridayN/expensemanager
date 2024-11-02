import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardCustom } from '../../models/mat-card';

@Component({
  selector: 'app-actions',
  standalone: false,
  templateUrl: './actions.component.html',
  styleUrl: './actions.component.css'
})
export class ActionsComponent {
  cards: Array<MatCardCustom>;

  constructor(private router: Router) {
    this.cards = new Array<MatCardCustom>();
  }

  ngOnInit() {
    this.cards = [
      { title: 'Manage-Expenses', showButton: true, buttonText: 'Manage-Expenses' },
      { title: 'Manage-Payment-Methods', showButton: true, buttonText: 'Manage-Expenses' }
    ];
  }
}
