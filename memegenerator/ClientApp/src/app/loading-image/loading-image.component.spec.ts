import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { LoadingImageComponent } from "./loading-image.component";
import { FileReaderFactory } from "../file-reader-factory/file-reader-factory";
import { MockEditImageComponent } from "../edit-image/edit-image-mock.component";

describe("LoadingImageComponent", () => {
  let component: LoadingImageComponent;
  let fixture: ComponentFixture<LoadingImageComponent>;
  let fileReaderFactoryMock: jasmine.SpyObj<FileReaderFactory>;
  const file = {};
  let fakeEvent;

  class FakeFileReader {
    onload: Function;

    readAsDataURL() { }
  }

  beforeEach(async(() => {
    fileReaderFactoryMock = jasmine.createSpyObj("loadingImageFactoryMock", ["createFileReader"]);
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
        { provide: FileReaderFactory, useValue: fileReaderFactoryMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoadingImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should create file reader", () => {
    const fakeFileReader = new FakeFileReader();

    fileReaderFactoryMock.createFileReader.and.returnValue(fakeFileReader);
    component.onImageIsLoaded(fakeEvent);

    expect(fileReaderFactoryMock.createFileReader).toHaveBeenCalled();
  });

  it("should read file on file event", () => {
    const fakeFileReader = new FakeFileReader();

    fileReaderFactoryMock.createFileReader.and.returnValue(fakeFileReader);
    component.onImageIsLoaded(fakeEvent);

    expect(fakeFileReader.onload).not.toBe(undefined);
  });

  it("should call EditImageComponent on image is loaded", () => {
    const fakeFileReader = new FakeFileReader();
    const image = "";
    const imageIsLoadedEvent = { target: { result: image } };

    spyOn(component.imageIsUploaded, "emit");

    fileReaderFactoryMock.createFileReader.and.returnValue(fakeFileReader);
    component.onImageIsLoaded(fakeEvent);
    fakeFileReader.onload(imageIsLoadedEvent);

    expect(component.imageIsUploaded.emit).toHaveBeenCalledWith(image);
  });

  it("should read file", () => {
    const fakeFileReader = new FakeFileReader();
    spyOn(fakeFileReader, "readAsDataURL");

    fileReaderFactoryMock.createFileReader.and.returnValue(fakeFileReader);
    component.onImageIsLoaded(fakeEvent);

    expect(fakeFileReader.readAsDataURL).toHaveBeenCalledWith(fakeEvent.target.files[0]);
    expect(fakeFileReader.readAsDataURL).not.toBe(undefined);
  });
});
