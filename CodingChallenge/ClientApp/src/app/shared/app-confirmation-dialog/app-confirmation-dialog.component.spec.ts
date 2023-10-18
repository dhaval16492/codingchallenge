import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppConfirmationDialogComponent } from './app-confirmation-dialog.component';

describe('AppConfirmationDialogComponent', () => {
  let component: AppConfirmationDialogComponent;
  let fixture: ComponentFixture<AppConfirmationDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppConfirmationDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppConfirmationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
