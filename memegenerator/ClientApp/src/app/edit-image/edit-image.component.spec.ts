import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { EditImageComponent } from './edit-image.component';
import { fabric } from "fabric";
import { FabricFactoryService } from '../fabric-factory.service';

let fabricFactoryMock: jasmine.SpyObj<FabricFactoryService>;
let fakeCanvas: jasmine.SpyObj<fabric.Canvas>;
let fakeImage: fabric.Image;
let fakeText: fabric.Text;

describe('EditImageComponent', () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;

  beforeEach(async(() => {
    fakeCanvas = jasmine.createSpyObj('fakeCanvas', ['setBackgroundImage', 'renderAll', 'getWidth', 'getHeight', 'add']);
    fabricFactoryMock = jasmine.createSpyObj('fabricFactoryMock', ['createCanvas', 'createImage', 'createText']);

    TestBed.configureTestingModule({
      declarations: [EditImageComponent],
      providers: [
        { provide: FabricFactoryService, useValue: fabricFactoryMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(EditImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render canvas', () => {
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('canvas'));
  });

  it('should set background image', (done) => {
    component.uploadedImageUrl = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==';

    let imageWidth = 400;
    let imageHeight = 500;
    let canvasWidth = 500;
    let canvasHeight = 600;

    fakeImage = <fabric.Image>(({ width: imageWidth, height: imageHeight }) as any);
    fabricFactoryMock.createImage.and.returnValue(fakeImage);

    fakeCanvas.getWidth.and.returnValue(canvasWidth);
    fakeCanvas.getHeight.and.returnValue(canvasHeight);

    fabricFactoryMock.createCanvas.and.returnValue(fakeCanvas);
    
    let promise = component.addImageToCanvas();
    promise.then(() => {
      expect(fakeCanvas.setBackgroundImage).toHaveBeenCalled();
      done();
    });
  });

  it('should add text to image', () => {
    component.onAddText();
    expect(component.onAddText).toHaveBeenCalled();
  });
});
