import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoUpdateComponent } from './info-update.component';

describe('InfoUpdateComponent', () => {
  let component: InfoUpdateComponent;
  let fixture: ComponentFixture<InfoUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InfoUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InfoUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
