import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TextEditorComponent } from './text-editor.component';
import { ColorPaletteMockComponent } from '../color-palette/color-palette-mock.component';
import { FabricFactory } from "../fabric-factory/fabric-factory";
import { fabric } from "fabric";

describe('TextEditorComponent', () => {
  let component: TextEditorComponent;
  let fixture: ComponentFixture<TextEditorComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactory>;
  let mockCanvas: jasmine.SpyObj<fabric.Canvas>;

  beforeEach(async(() => {
    fabricFactoryMock = jasmine.createSpyObj("fabricFactoryMock", ["createText", "createCanvas"]);

    mockCanvas = jasmine.createSpyObj("mockCanvas", [
      "getWidth",
      "add",
      "remove",
      "renderAll",
      "getActiveObject",
      "setActiveObject"
    ]);

    fabricFactoryMock.createCanvas.and.returnValue(mockCanvas);

    TestBed.configureTestingModule({
      declarations: [
        TextEditorComponent,
        ColorPaletteMockComponent
      ],
      providers: [
        { provide: FabricFactory, useValue: fabricFactoryMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TextEditorComponent);
    component = fixture.componentInstance;

    component.canvas = mockCanvas;

    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add text to image', () => {
    const canvasWidth = 500;
    mockCanvas.getWidth.and.returnValue(canvasWidth);

    component.addText();

    expect(mockCanvas.add).toHaveBeenCalledWith(
      fabricFactoryMock.createText("Sample\ntext",
      mockCanvas.getWidth(),
      component.textColor,
      component.outlineColor
    ));
  });

  it('should delete selected text', () => {
    mockCanvas.setActiveObject(new fabric.IText("ActiveText"));

    component.deleteSelectedText();

    expect(mockCanvas.remove).toHaveBeenCalledWith(mockCanvas.getActiveObject());
  });

  it('should change the text color if the text was selected', () => {
    let activeTextSpy = new fabric.IText("active text");
    spyOn(activeTextSpy, "setColor");
    mockCanvas.getActiveObject.and.returnValue(activeTextSpy);

    component.textColorChange();

    expect(activeTextSpy.setColor).toHaveBeenCalledWith(component.textColor);
    expect(mockCanvas.renderAll).toHaveBeenCalled();
  });

  it('should change the outline color if the text was selected', () => {
    let activeTextSpy = new fabric.IText("active text");
    spyOn(activeTextSpy, "set");
    mockCanvas.getActiveObject.and.returnValue(activeTextSpy);

    component.outlineColorChange();

    expect(activeTextSpy.set).toHaveBeenCalledWith("stroke", component.outlineColor);
    expect(mockCanvas.renderAll).toHaveBeenCalled();
  });
});
