import { Component, Input } from '@angular/core';
import { MatCardCustom } from '../../models/mat-card';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mat-card',
  templateUrl: './mat-card.component.html',
  styleUrl: './mat-card.component.css'
})
export class MatCardComponent {
  @Input() cardsList: Array<MatCardCustom> | undefined;

  constructor(private router: Router) {}
  routeByAction(action: string) {
    // alert('routing to /' + action.toLowerCase());
    this.router.navigateByUrl(action.toLowerCase());
  }
}
