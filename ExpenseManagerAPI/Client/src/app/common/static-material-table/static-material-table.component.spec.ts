import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaticMaterialTableComponent } from './static-material-table.component';

describe('StaticMaterialTableComponent', () => {
  let component: StaticMaterialTableComponent;
  let fixture: ComponentFixture<StaticMaterialTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaticMaterialTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StaticMaterialTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
