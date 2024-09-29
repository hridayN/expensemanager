import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-actions',
  standalone: false,
  templateUrl: './actions.component.html',
  styleUrl: './actions.component.css'
})
export class ActionsComponent {
  actions = Array<string>();
  constructor(private router: Router) {
    this.actions = new Array<string>();
  }
  ngOnInit() {
    this.actions = ['Manage-Expenses', 'Manage-Payment-Methods'];
  }

  routeByAction(action: string) {
    // alert('routing to /' + action.toLowerCase());
    this.router.navigateByUrl(action.toLowerCase());
  }
}
