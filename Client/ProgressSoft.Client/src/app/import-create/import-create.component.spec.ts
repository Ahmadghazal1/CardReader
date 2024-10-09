import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportCreateComponent } from './import-create.component';

describe('ImportCreateComponent', () => {
  let component: ImportCreateComponent;
  let fixture: ComponentFixture<ImportCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ImportCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ImportCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
