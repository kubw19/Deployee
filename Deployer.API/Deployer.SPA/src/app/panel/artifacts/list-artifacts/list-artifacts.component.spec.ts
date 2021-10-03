import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListArtifactsComponent } from './list-artifacts.component';

describe('ListArtifactsComponent', () => {
  let component: ListArtifactsComponent;
  let fixture: ComponentFixture<ListArtifactsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListArtifactsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListArtifactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
