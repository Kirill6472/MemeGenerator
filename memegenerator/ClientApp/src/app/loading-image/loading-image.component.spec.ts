import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingImageComponent } from './loading-image.component';

describe('LoadingImageComponent', () => {
  let component: LoadingImageComponent;
  let fixture: ComponentFixture<LoadingImageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        LoadingImageComponent
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadingImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render input tag with type file', () => {
    fixture = TestBed.createComponent(LoadingImageComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('input'));
  });

  it('should load image', () => {
    fixture = TestBed.createComponent(LoadingImageComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('img').src).toContain(component.uploadedImageUrl);
  });
});
