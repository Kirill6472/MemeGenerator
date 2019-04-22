import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditImageComponent } from './edit-image.component';
import "fabric";

declare var fabric: any;

var fakeCanvas = {
  setBackgroundImage: () => { }
};

describe('EditImageComponent', () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;

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
    fabric = {
      Canvas: fabric.Canvas = fakeCanvas
    };

    spyOn(fabric.Canvas, "setBackgroundImage");
    fabric.Canvas.setBackgroundImage();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render canvas', () => {
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('canvas'));
  });

  it('should add background image', () => {
    //let obj = jasmine.createSpyObj('fakeCanvas', ['setBackgroundImage', 'renderAll', 'getWidth', 'getHeight']);
    //obj.renderAll.and.return();

    let promise = component.addImageToCanvas();
    promise.then(() => {
      expect(fabric.Canvas.setBackgroundImage).toHaveBeenCalled();
    });
  });
});
