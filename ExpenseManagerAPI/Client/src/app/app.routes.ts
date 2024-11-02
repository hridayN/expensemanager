import { Routes } from '@angular/router';
import { ActionsComponent } from './components/actions/actions.component';
import { ExpenseManagerComponent } from './components/expense-manager/expense-manager.component';
import { PaymentMethodManagerComponent } from './components/payment-method-manager/payment-method-manager.component';
import { DynamicMaterialTableComponent } from './common/dynamic-material-table/dynamic-material-table.component';
import { StaticMaterialTableComponent } from './common/static-material-table/static-material-table.component';

export const routes: Routes = [
    { path: 'actions', component: ActionsComponent },
    { path: 'manage-expenses', component: ExpenseManagerComponent },
    { path: 'manage-payment-methods', component: PaymentMethodManagerComponent },
    { path: 'static', component: StaticMaterialTableComponent },
    { path: 'dynamic', component: DynamicMaterialTableComponent },
    { path: '', redirectTo: 'actions', pathMatch: 'full' },
    { path: '**', redirectTo: 'actions', pathMatch: 'full' }
];
