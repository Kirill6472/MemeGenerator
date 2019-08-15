import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextEditorComponent } from './text-editor.component';
import { FabricFactory } from '../fabric-factory/fabric-factory';
import { fabric } from 'fabric';
import { ColorPaletteComponent } from '../color-palette/color-palette.component';

describe('TextEditorComponent', () => {
  let component: TextEditorComponent;
  let fixture: ComponentFixture<TextEditorComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactory>;
  let mockCanvas: jasmine.SpyObj<fabric.Canvas>;
  let mockText: jasmine.SpyObj<fabric.IText>;

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
        ColorPaletteComponent
      ],
      providers: [
        { provide: FabricFactory, useValue: fabricFactoryMock }
      ]
    })
    .overrideComponent(ColorPaletteComponent, {
      set: {
        selector: 'app-color-palette',
        template: '<div></div>'
      }
    })
    .compileComponents();

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

    mockText = jasmine.createSpyObj('mockText', ["on"]);
    fabricFactoryMock.createText.and.returnValue(mockText);
   
    component.addText();

    expect(mockCanvas.add).toHaveBeenCalledWith(mockText);
    expect(mockText.on).toHaveBeenCalled();
  });

  it('should delete selected text', () => {
    mockCanvas.setActiveObject(new fabric.IText("ActiveText"));

    component.deleteSelectedText();

    expect(mockCanvas.remove).toHaveBeenCalledWith(mockCanvas.getActiveObject());
  });

  it('should change the text color if the text was selected', () => {
    let activeText = new fabric.IText("active text");
    spyOn(activeText, "setColor");
    mockCanvas.getActiveObject.and.returnValue(activeText);

    component.textColorChange();

    expect(activeText.setColor).toHaveBeenCalledWith(component.textColor);
    expect(mockCanvas.renderAll).toHaveBeenCalled();
  });

  it('should change the outline color if the text was selected', () => {
    let activeText = new fabric.IText("active text");
    spyOn(activeText, "set");
    mockCanvas.getActiveObject.and.returnValue(activeText);

    component.outlineColorChange();

    expect(activeText.set).toHaveBeenCalledWith("stroke", component.outlineColor);
    expect(mockCanvas.renderAll).toHaveBeenCalled();
  });

  it('should set palette with colors of active text', () => {
    let activeText = new fabric.IText("active text");
    let onSpy = spyOn(activeText, "on");
    fabricFactoryMock.createText.and.returnValue(activeText);

    const fakeColor = "#000000";
    activeText.setColor(fakeColor);
    activeText.set("stroke", fakeColor);
    mockCanvas.getActiveObject.and.returnValue(activeText);

    component.addText();

    let setPaletteWithColorsOfActiveText = onSpy.calls.argsFor(0)[1];
    setPaletteWithColorsOfActiveText();

    expect(component.textColor).toContain(fakeColor);
    expect(component.outlineColor).toContain(fakeColor);
  });
});
