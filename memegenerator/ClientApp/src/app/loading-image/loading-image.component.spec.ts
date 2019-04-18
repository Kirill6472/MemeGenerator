import { Component } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { LoadingImageComponent } from './loading-image.component';

@Component({ selector: 'app-edit-image', template: '' })
class EditImageComponent { }

describe('LoadingImageComponent', () => {
  let component: LoadingImageComponent;
  let fixture: ComponentFixture<LoadingImageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        LoadingImageComponent,
        EditImageComponent
      ],
      providers: [{ provide: EditImageComponent, useValue: editImageComponentStub }]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadingImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  const editImageComponentStub = {
    addImageToCanvas: () => { },
    hideImageLoading: () => { }
  };


  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render input tag with type file', () => {
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('input'));
  });

  it('should load image and add to canvas', () => {
    spyOn(editImageComponentStub, 'addImageToCanvas');
    editImageComponentStub.addImageToCanvas();
    fixture.detectChanges();
    expect(editImageComponentStub.addImageToCanvas).toHaveBeenCalled();
  });

  it('should hide image loading element', () => {
    spyOn(editImageComponentStub, 'hideImageLoading');
    editImageComponentStub.hideImageLoading();
    fixture.detectChanges();
    expect(editImageComponentStub.hideImageLoading).toHaveBeenCalled();
  });
});
