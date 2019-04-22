import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { EditImageComponent } from './edit-image.component';
import { fabric } from "fabric";
import { FabricFactoryService } from '../fabric-factory.service';

let fabricFactoryMock: jasmine.SpyObj<FabricFactoryService>;
let fakeCanvas: jasmine.SpyObj<fabric.Canvas>;
let fakeImage: jasmine.SpyObj<fabric.Image>;
let imageInstance: any;

describe('EditImageComponent', () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;

  beforeEach(async(() => {
    fakeCanvas = jasmine.createSpyObj('fakeCanvas', ['setBackgroundImage', 'renderAll', 'getWidth', 'getHeight']);
    fabricFactoryMock = jasmine.createSpyObj('factoryMock', ['createCanvas', 'createImage']);
    fabricFactoryMock.createCanvas.and.returnValue(fakeCanvas);
    fabricFactoryMock.createImage.and.returnValue(fakeImage);

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

  it('should set background image', () => {
    let promise = component.addImageToCanvas();
    promise.then(() => {
      imageInstance = fakeImage;
      expect(fakeCanvas.setBackgroundImage).toHaveBeenCalledWith(imageInstance, fakeCanvas.renderAll.bind(fakeCanvas), {
        scaleY: fakeCanvas.getHeight() / imageInstance.height,
        scaleX: fakeCanvas.getWidth() / imageInstance.width,
        selectable: false
      });
    });
  });
});
