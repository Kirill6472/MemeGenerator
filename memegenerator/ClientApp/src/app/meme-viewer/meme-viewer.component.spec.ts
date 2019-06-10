import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MemeViewerComponent } from './meme-viewer.component';

describe('MemeViewerComponent', () => {
  let component: MemeViewerComponent;
  let fixture: ComponentFixture<MemeViewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemeViewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemeViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
