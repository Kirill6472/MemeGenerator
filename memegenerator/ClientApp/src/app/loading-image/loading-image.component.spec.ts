import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { Component } from "@angular/core";
import { LoadingImageComponent } from "./loading-image.component";
import { EditImageComponent } from "../edit-image/edit-image.component";
import { LoadingImageFactoryService } from "../loading-image-factory.service";

@Component({
  selector: "app-edit-image",
  template: "",
  providers: [{
    provide: EditImageComponent,
    useClass: MockEditImageComponent
  }]
})
class MockEditImageComponent {
  addImageToCanvas() { };
  hideImageLoading() { };
}

describe("LoadingImageComponent", () => {
  let component: LoadingImageComponent;
  let fixture: ComponentFixture<LoadingImageComponent>;
  let loadingImageFactoryMock: jasmine.SpyObj<LoadingImageFactoryService>;

  class FakeFileReader {
    onload: Function;

    readAsDataURL() { }
  }

  beforeEach(async(() => {
    loadingImageFactoryMock = jasmine.createSpyObj("loadingImageFactoryMock", ["createFileReader"]);

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
    const file = {};
    const fakeEvent = {
      target: {
        files: [file]
      }
    };

    const fakeFileReader = new FakeFileReader();
    const readAsDataURLspy = spyOn(fakeFileReader, "readAsDataURL");
    loadingImageFactoryMock.createFileReader.and.returnValue(fakeFileReader);

    component.onImageIsLoaded(fakeEvent);

    expect(fakeFileReader.onload).not.toBe(undefined);
  });

  it("should call EditImageComponent on image is loaded", () => {
    const file = {};
    const fakeEvent = {
      target: {
        files: [file]
      }
    };

    const fakeFileReader = new FakeFileReader();
    const readAsDataURLspy = spyOn(fakeFileReader, "readAsDataURL");
    loadingImageFactoryMock.createFileReader.and.returnValue(fakeFileReader);

    component.onImageIsLoaded(fakeEvent);

    const image = "";
    const imageIsLoadedEvent = { target: { result: image } };

    spyOn(component.editImage, "addImageToCanvas");
    spyOn(component.editImage, "hideImageLoading");

    fakeFileReader.onload(imageIsLoadedEvent);

    expect(component.editImage.uploadedImageUrl).toBe(image);
    expect(component.editImage.addImageToCanvas).toHaveBeenCalled();
    expect(component.editImage.hideImageLoading).toHaveBeenCalled();
  });
});
