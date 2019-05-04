import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { LoadingImageComponent } from "./loading-image.component";
import { LoadingImageFactoryService } from "../loading-image-factory/loading-image-factory.service";
import { MockEditImageComponent } from "../edit-image/MockEditImageComponent";

describe("LoadingImageComponent", () => {
  let component: LoadingImageComponent;
  let fixture: ComponentFixture<LoadingImageComponent>;
  let loadingImageFactoryMock: jasmine.SpyObj<LoadingImageFactoryService>;
  const file = {};
  let fakeEvent;

  class FakeFileReader {
    onload: Function;

    readAsDataURL() { }
  }

  beforeEach(async(() => {
    loadingImageFactoryMock = jasmine.createSpyObj("loadingImageFactoryMock", ["createFileReader"]);
    fakeEvent = {
      target: {
        files: [file]
      }
    };

    TestBed.configureTestingModule({
      declarations: [
        LoadingImageComponent,
        MockEditImageComponent
      ],
      providers: [
        { provide: LoadingImageFactoryService, useValue: loadingImageFactoryMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoadingImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should render input tag with type file", () => {
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector("input"));
  });

  it("should read file if event is file uploading", () => {
    const fakeEvent = {
      target: {
        files: 0
      }
    };

    loadingImageFactoryMock.createFileReader();
    component.onImageIsLoaded(fakeEvent);

    expect(loadingImageFactoryMock.createFileReader).toHaveBeenCalled();
  });

  it("should read file on file event", () => {
    const fakeFileReader = new FakeFileReader();

    loadingImageFactoryMock.createFileReader.and.returnValue(fakeFileReader);
    component.onImageIsLoaded(fakeEvent);

    expect(fakeFileReader.onload).not.toBe(undefined);
  });

  it("should call EditImageComponent on image is loaded", () => {
    const fakeFileReader = new FakeFileReader();
    const image = "";
    const imageIsLoadedEvent = { target: { result: image } };

    spyOn(component.imageIsUploaded, "emit");

    loadingImageFactoryMock.createFileReader.and.returnValue(fakeFileReader);
    component.onImageIsLoaded(fakeEvent);
    fakeFileReader.onload(imageIsLoadedEvent);

    expect(component.imageIsUploaded.emit).toHaveBeenCalledWith(image);
  });
});
