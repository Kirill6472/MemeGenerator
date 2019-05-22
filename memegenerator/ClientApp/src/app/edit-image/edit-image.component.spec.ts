import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { EditImageComponent } from "./edit-image.component";
import { fabric } from "fabric";
import { FabricFactory } from "../fabric-factory/fabric-factory";

describe("EditImageComponent", () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactory>;
  let mockCanvas: jasmine.SpyObj<fabric.Canvas>;
  let fakeEvent;

  beforeEach(async(() => {
    mockCanvas = jasmine.createSpyObj("fakeCanvas", [
      "setBackgroundImage",
      "renderAll",
      "getWidth",
      "getHeight",
      "add",
      "remove",
      "setActiveObject",
      "getActiveObject",
      "toDataURL",
      "clear",
      "item"
    ]);

    fabricFactoryMock = jasmine.createSpyObj("fabricFactoryMock", [
      "createCanvas",
      "createImage",
      "createText"
    ]);

    fabricFactoryMock.createCanvas.and.returnValue(mockCanvas);

    fakeEvent = {
      target: {
        value: "#000000"
      }
    }

    TestBed.configureTestingModule({
      declarations: [
        EditImageComponent
      ],
      providers: [
        { provide: FabricFactory, useValue: fabricFactoryMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(EditImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should set background image", (done) => {
    const uploadedImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";
    const imageWidth = 400;
    const imageHeight = 500;
    const canvasWidth = 500;
    const canvasHeight = 600;
    const mockImage = <fabric.Image>(({ width: imageWidth, height: imageHeight }) as any);

    fabricFactoryMock.createImage.and.returnValue(mockImage);
    mockCanvas.getWidth.and.returnValue(canvasWidth);
    mockCanvas.getHeight.and.returnValue(canvasHeight);

    const promise = component.setImage(uploadedImageUrl);
    promise.then(() => {
      expect(mockCanvas.setBackgroundImage).toHaveBeenCalled();
      done();
    });
    expect(component.isToolbarShown).toBe(true);
  });

  it("should add text to image", () => {
    let mockText = fabric.IText;
    fabricFactoryMock.createText.and.returnValue(mockText);

    component.addText();

    expect(mockCanvas.add).toHaveBeenCalledWith(mockText);
  });

  it("should remove text from image", () => {
    let activeTextSpy = jasmine.createSpy();
    mockCanvas.getActiveObject.and.returnValue(activeTextSpy);

    component.deleteSelectedText();

    expect(mockCanvas.remove).toHaveBeenCalledWith(mockCanvas.getActiveObject());
  });

  it("should generate and preview meme", () => {
    component.generateAndPreviewMeme();

    expect(mockCanvas.toDataURL).toHaveBeenCalled();
    expect(component.isMemePreview).toBe(true);
  });

  it("should display meme creation", () => {
    spyOn(component.isImageLoaded, "emit");

    component.createNewMeme();

    expect(mockCanvas.clear).toHaveBeenCalled();
    expect(component.isMemePreview).toBe(false);
    expect(component.isToolbarShown).toBe(false);
    expect(component.isImageLoaded.emit).toHaveBeenCalledWith(false);
  });

  it("should change text color", () => {  
    let activeTextSpy = jasmine.createSpyObj(["setColor"]);
    mockCanvas.getActiveObject.and.returnValue(activeTextSpy);
    
    component.onChangeTextColor(fakeEvent);

    expect(activeTextSpy.setColor).toHaveBeenCalledWith(fakeEvent.target.value);
    expect(mockCanvas.renderAll).toHaveBeenCalled();
  });

  it("should change outline color", () => {
    let activeTextSpy = jasmine.createSpyObj(["set"]);
    mockCanvas.getActiveObject.and.returnValue(activeTextSpy);

    component.onChangeOutlineColor(fakeEvent);

    expect(activeTextSpy.set).toHaveBeenCalledWith("stroke", fakeEvent.target.value);
    expect(mockCanvas.renderAll).toHaveBeenCalled();
  });

  it("should return color list", () => {
    expect(component.getTextColors()).not.toBe(null);
  });
});
