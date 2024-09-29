import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentMethodManagerComponent } from './payment-method-manager.component';

describe('PaymentMethodManagerComponent', () => {
  let component: PaymentMethodManagerComponent;
  let fixture: ComponentFixture<PaymentMethodManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PaymentMethodManagerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PaymentMethodManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
