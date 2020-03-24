import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogSummitComponent } from './dialog-summit.component';

describe('DialogSummitComponent', () => {
  let component: DialogSummitComponent;
  let fixture: ComponentFixture<DialogSummitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogSummitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogSummitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
