import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { EditImageComponent } from "./edit-image.component";
import { fabric } from "fabric";
import { FabricFactoryService } from "../fabric-factory/fabric-factory.service";

describe("EditImageComponent", () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactoryService>;
  let mockCanvas: jasmine.SpyObj<fabric.Canvas>;

  beforeEach(async(() => {
    mockCanvas = jasmine.createSpyObj("fakeCanvas", [
      "setBackgroundImage",
      "renderAll",
      "getWidth",
      "getHeight",
      "add",
      "remove",
      "getActiveObject",
      "toDataURL",
      "clear"
    ]);

    fabricFactoryMock = jasmine.createSpyObj("fabricFactoryMock", [
      "createCanvas",
      "createImage",
      "createText"
    ]);

    fabricFactoryMock.createCanvas.and.returnValue(mockCanvas);

    TestBed.configureTestingModule({
      declarations: [
        EditImageComponent
      ],
      providers: [
        { provide: FabricFactoryService, useValue: fabricFactoryMock }
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
    let mockImage: fabric.Image;
    const uploadedImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";
    const imageWidth = 400;
    const imageHeight = 500;
    const canvasWidth = 500;
    const canvasHeight = 600;

    mockImage = <fabric.Image>(({ width: imageWidth, height: imageHeight }) as any);
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
});
