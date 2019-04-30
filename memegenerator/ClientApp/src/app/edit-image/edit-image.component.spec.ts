import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { EditImageComponent } from "./edit-image.component";
import { fabric } from "fabric";
import { FabricFactoryService } from "../fabric-factory.service";

describe("EditImageComponent", () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactoryService>;
  let mockCanvas: jasmine.SpyObj<fabric.Canvas>;
  let mockImage: fabric.Image;

  beforeEach(async(() => {
    mockCanvas = jasmine.createSpyObj("fakeCanvas", [
      "setBackgroundImage",
      "renderAll",
      "getWidth",
      "getHeight",
      "add",
      "remove",
      "getActiveObject",
      "toDataURL"
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

  it("should render canvas", () => {
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector("canvas"));
  });

  it("should set background image", (done) => {
    const uploadedImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";

    const imageWidth = 400;
    const imageHeight = 500;
    const canvasWidth = 500;
    const canvasHeight = 600;

    mockImage = <fabric.Image>(({ width: imageWidth, height: imageHeight }) as any);
    fabricFactoryMock.createImage.and.returnValue(mockImage);

    mockCanvas.getWidth.and.returnValue(canvasWidth);
    mockCanvas.getHeight.and.returnValue(canvasHeight);

    const promise = component.addImageToCanvas(uploadedImageUrl);
    promise.then(() => {
      expect(mockCanvas.setBackgroundImage).toHaveBeenCalled();
      done();
    });
  });

  it("should add text to image", () => {
    component.getCanvas();

    component.onAddText();   
    expect(mockCanvas.add).toHaveBeenCalled();
  });

  it("should remove text from image", () => {
    component.getCanvas();

    component.onDeleteText();
    expect(mockCanvas.remove).toHaveBeenCalledWith(mockCanvas.getActiveObject());
  });

  it("should show meme preview", () => {
    component.showMemePreview();
    expect(component.displayMemePreview).toBe(true);
  });

  it("should generate meme", () => {
    component.getCanvas();

    component.generateMeme();
    expect(mockCanvas.toDataURL).toHaveBeenCalled();
  });

  it("should generate and display meme", () => {
    spyOn(component, "showMemePreview");
    spyOn(component, "generateMeme");

    component.onGenerateAndDisplayMeme();

    expect(component.showMemePreview).toHaveBeenCalled();
    expect(component.generateMeme).toHaveBeenCalled();
  });

  it("should display meme creation", () => {
    spyOn(component.imageLoading, "emit");

    component.onCreateNewMeme();

    expect(component.displayMemePreview).toBe(false);
    expect(component.imageLoading.emit).toHaveBeenCalledWith(false);
  });
});
