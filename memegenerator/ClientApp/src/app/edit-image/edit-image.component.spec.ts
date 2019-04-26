import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { EditImageComponent } from "./edit-image.component";
import { fabric } from "fabric";
import { FabricFactoryService } from "../fabric-factory.service";

describe("EditImageComponent", () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactoryService>;
  let fakeCanvas: jasmine.SpyObj<fabric.Canvas>;
  let fakeImage: fabric.Image;

  beforeEach(async(() => {
    fakeCanvas = jasmine.createSpyObj("fakeCanvas", ["setBackgroundImage", "renderAll", "getWidth", "getHeight", "add", "remove", "getActiveObject", "toDataURL"]);
    fabricFactoryMock = jasmine.createSpyObj("fabricFactoryMock", ["createCanvas", "createImage", "createText"]);

    fabricFactoryMock.createCanvas.and.returnValue(fakeCanvas);

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
    component.uploadedImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";

    const imageWidth = 400;
    const imageHeight = 500;
    const canvasWidth = 500;
    const canvasHeight = 600;

    fakeImage = <fabric.Image>(({ width: imageWidth, height: imageHeight }) as any);
    fabricFactoryMock.createImage.and.returnValue(fakeImage);

    fakeCanvas.getWidth.and.returnValue(canvasWidth);
    fakeCanvas.getHeight.and.returnValue(canvasHeight);

    const promise = component.addImageToCanvas();
    promise.then(() => {
      expect(fakeCanvas.setBackgroundImage).toHaveBeenCalled();
      done();
    });
  });

  it("should add text to image", () => {
    component.getCanvas();

    component.onAddText();   
    expect(fakeCanvas.add).toHaveBeenCalled();
  });

  it("should remove text from image", () => {
    component.getCanvas();

    component.onDeleteText();
    expect(fakeCanvas.remove).toHaveBeenCalledWith(fakeCanvas.getActiveObject());
  });

  it("should hide image loading", () => {
    component.hideImageLoading();
    expect(component.showUploadedImage).toBe(true);
  });

  it("should show meme preview", () => {
    component.showMemePreview();
    expect(component.displayMemePreview).toBe(true);
  });

  it("should generate meme", () => {
    component.getCanvas();

    component.generateMeme();
    expect(fakeCanvas.toDataURL).toHaveBeenCalled();
  });

  it("should generate and display meme", () => {
    spyOn(component, "showMemePreview");
    spyOn(component, "generateMeme");

    component.onGenerateAndDisplayMeme();

    expect(component.showMemePreview).toHaveBeenCalled();
    expect(component.generateMeme).toHaveBeenCalled();
  });

  it("should display meme creation", () => {
    component.onCreateNewMeme();
    expect(component.displayMemePreview).toBe(false);
    expect(component.showUploadedImage).toBe(false);
  });
});
