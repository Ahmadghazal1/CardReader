import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManualCreateComponent } from './manual-create.component';

describe('ManualCreateComponent', () => {
  let component: ManualCreateComponent;
  let fixture: ComponentFixture<ManualCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManualCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManualCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
