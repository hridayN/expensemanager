import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { ActionsComponent } from "./components/actions/actions.component";
import { ExpenseManagerComponent } from "./components/expense-manager/expense-manager.component";
import { PaymentMethodManagerComponent } from "./components/payment-method-manager/payment-method-manager.component";
import { Router } from "@angular/router";

// NOTE: import/export modules here, declare components here only
@NgModule({
    imports: [
        ReactiveFormsModule,
        CommonModule
    ],
    declarations: [
        ActionsComponent,
        ExpenseManagerComponent,
        PaymentMethodManagerComponent
    ],
    exports: [
        ReactiveFormsModule,
        CommonModule
    ]
})

export class AppCommonModule { }