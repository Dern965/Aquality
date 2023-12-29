import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UbicanosComponent } from './ubicanos.component';

describe('UbicanosComponent', () => {
  let component: UbicanosComponent;
  let fixture: ComponentFixture<UbicanosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UbicanosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UbicanosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
