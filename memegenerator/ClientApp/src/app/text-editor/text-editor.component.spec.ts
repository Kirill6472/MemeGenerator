import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TextEditorComponent } from './text-editor.component';
import { ColorPaletteMockComponent } from '../color-palette/color-palette-mock.component';
import { FabricFactory } from "../fabric-factory/fabric-factory";
import { fabric } from "fabric";

describe('TextEditorComponent', () => {
  let component: TextEditorComponent;
  let fixture: ComponentFixture<TextEditorComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactory>;
  let expectedCanvas: fabric.Canvas;
  let mockColor: string;

  beforeEach(async(() => {
    fabricFactoryMock = jasmine.createSpyObj("fabricFactoryMock", ["createText"]);
    mockColor = "#FFFFFF";

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

    expectedCanvas = new fabric.Canvas('canvas');
    component.canvas = expectedCanvas;

    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add text to image', () => {
    spyOn(component.canvas, "add");
    component.canvas.setWidth(600);

    component.addText();

    expect(component.canvas.add).toHaveBeenCalledWith(
      fabricFactoryMock.createText("Sample\ntext",
      component.canvas.getWidth(),
      component.textColor,
      component.outlineColor
    ));
  });

  it('should delete selected text', () => {
    component.canvas.setActiveObject(new fabric.IText("ActiveText"));
    spyOn(component.canvas, "remove");

    component.deleteSelectedText();

    expect(component.canvas.remove).toHaveBeenCalledWith(component.canvas.getActiveObject());
  });

  it('should change the text color if the text was selected', () => {
    component.canvas.setActiveObject(new fabric.IText("ActiveText"));
    spyOn(component.canvas.getActiveObject(), "setColor");
    spyOn(component.canvas, "renderAll");

    component.textColorChange(mockColor);

    expect(component.textColor).toContain(mockColor);
    expect(component.canvas.getActiveObject().type).toContain("i-text");
    expect(component.canvas.getActiveObject().setColor).toHaveBeenCalledWith(component.textColor);
    expect(component.canvas.renderAll).toHaveBeenCalled();
  });

  it('should update the value of the variable textColor if the text was not selected', () => {
    component.canvas.setActiveObject(new fabric.Circle());

    component.textColorChange(mockColor);

    expect(component.textColor).toContain(mockColor);
    expect(component.canvas.getActiveObject().type).not.toContain("i-text");
  });

  it('should change the outline color if the text was selected', () => {
    component.canvas.setActiveObject(new fabric.IText("ActiveText"));
    spyOn(component.canvas.getActiveObject(), "set");
    spyOn(component.canvas, "renderAll");

    component.outlineColorChange(mockColor);

    expect(component.outlineColor).toContain(mockColor);
    expect(component.canvas.getActiveObject().type).toContain("i-text");
    expect(component.canvas.getActiveObject().set).toHaveBeenCalledWith("stroke", component.outlineColor);
    expect(component.canvas.renderAll).toHaveBeenCalled();
  });

  it('should change the value of the variable outlineColor if the text was not selected', () => {
    component.canvas.setActiveObject(new fabric.Circle());

    component.outlineColorChange(mockColor);

    expect(component.outlineColor).toContain(mockColor);
    expect(component.canvas.getActiveObject().type).not.toContain("i-text");
  });
});
