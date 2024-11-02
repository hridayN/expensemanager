import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ActionsComponent } from "./components/actions/actions.component";
import { ExpenseManagerComponent } from "./components/expense-manager/expense-manager.component";
import { PaymentMethodManagerComponent } from "./components/payment-method-manager/payment-method-manager.component";
import { Router } from "@angular/router";
import { MaterialModule } from "./material.module";
import { HttpClientModule } from "@angular/common/http";
import { BrowserModule } from "@angular/platform-browser";
import { MatCardComponent } from "./common/mat-card/mat-card.component";
import { StaticMaterialTableComponent } from "./common/static-material-table/static-material-table.component";
import { DynamicMaterialTableComponent } from "./common/dynamic-material-table/dynamic-material-table.component";

// NOTE: import/export modules here, declare components here only
@NgModule({
    imports: [
        BrowserModule,
        // BrowserAnimationsModule,
        FormsModule,
        HttpClientModule,
        MaterialModule,
        // MatNativeDateModule,
        ReactiveFormsModule,
        CommonModule
    ],
    declarations: [
        ActionsComponent,
        ExpenseManagerComponent,
        PaymentMethodManagerComponent,
        MatCardComponent,
        StaticMaterialTableComponent,
        DynamicMaterialTableComponent
    ],
    exports: [
        ReactiveFormsModule,
        CommonModule,

    ]
})

export class AppCommonModule { }