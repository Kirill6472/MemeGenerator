import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { EditImageComponent } from "./edit-image.component";
import { fabric } from "fabric";
import { FabricFactory } from "../fabric-factory/fabric-factory";
import { TextEditorComponent } from "../text-editor/text-editor.component";

describe("EditImageComponent", () => {
  let component: EditImageComponent;
  let fixture: ComponentFixture<EditImageComponent>;
  let fabricFactoryMock: jasmine.SpyObj<FabricFactory>;
  let mockCanvas: jasmine.SpyObj<fabric.Canvas>;

  beforeEach(async(() => {
    mockCanvas = jasmine.createSpyObj("mockCanvas", [
      "setBackgroundImage",
      "renderAll",
      "getWidth",
      "getHeight",
      "getActiveObject",
      "toDataURL",
      "clear"
    ]);

    fabricFactoryMock = jasmine.createSpyObj("fabricFactoryMock", [
      "createCanvas",
      "createImage"
    ]);

    fabricFactoryMock.createCanvas.and.returnValue(mockCanvas);

    TestBed.configureTestingModule({
      declarations: [
        EditImageComponent,
        TextEditorComponent
      ],
      providers: [
        { provide: FabricFactory, useValue: fabricFactoryMock }
      ]
    })
    .overrideComponent(TextEditorComponent, {
      set: {
        selector: 'app-text-editor',
        template: '<div></div>'
      }
    })
    .compileComponents();

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

    expect(mockCanvas.clear).toHaveBeenCalled();
  });

  it("should generate meme", () => {
    spyOn(component.generatedMemeUrl, "emit");

    component.generateMeme();

    expect(component.generatedMemeUrl.emit).toHaveBeenCalledWith(mockCanvas.getActiveObject());
  });
});
