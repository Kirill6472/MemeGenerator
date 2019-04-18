import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditImageComponent } from './edit-image.component';
import "fabric";
declare const fabric: any;

describe('EditImageComponent', () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;
  var yFabricService;
  var fakeFabric;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditImageComponent ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  beforeEach(() => {
    fakeFabric = {
      canvas: fabric.Canvas
    };
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render canvas', () => {
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('canvas'));
  });

  it('should load image', () => {
    var image = new Image();
    image.onload = () => {
      fakeFabric.canvas.setBackgroundImage(component.imageInstance, fakeFabric.canvas.renderAll.bind(fakeFabric.canvas), {
        scaleY: fakeFabric.canvas.getHeight() / component.imageInstance.height,
        scaleX: fakeFabric.canvas.getWidth() / component.imageInstance.width,
        selectable: false
      });
    }
    image.src = component.uploadedImageUrl;

    expect(fakeFabric.canvas.setBackgroundImage).toContain(component.canvas.setBackgroundImage);
  });
});
