import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { LoadingImageComponent } from "./loading-image.component";
import { EditImageComponent } from "../edit-image/edit-image.component";
import { LoadingImageFactoryService } from "../loading-image-factory.service";

describe("LoadingImageComponent", () => {
  let component: LoadingImageComponent;
  let fixture: ComponentFixture<LoadingImageComponent>;
  let editImageComponentMock: jasmine.SpyObj<EditImageComponent>;
  let fakeEvent: jasmine.SpyObj<any>;
  let loadingImageFactoryMock: jasmine.SpyObj<LoadingImageFactoryService>;

  beforeEach(async(() => {
    editImageComponentMock = jasmine.createSpyObj("editImageComponentMock", ["addImageToCanvas", "hideImageLoading"]);
    fakeEvent = jasmine.createSpyObj("fakeEvent", ["target"]);
    loadingImageFactoryMock = jasmine.createSpyObj("loadingImageFactoryMock", ["createFileReader"]);

    let mockReader = loadingImageFactoryMock.createFileReader();

    TestBed.configureTestingModule({
      declarations: [
        LoadingImageComponent,
        EditImageComponent
      ],
      providers: [
        { provide: EditImageComponent, useValue: editImageComponentMock }
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

  it("should load image and add to canvas", () => {
    const mockFile = new File([""], "filename", { type: "image/jpg" });

    spyOn(this.mockReader, "onload");
    spyOn(this.mockReader, "readAsDataURL");

    component.onImageIsLoaded(fakeEvent);

    expect(this.mockReader.onload).toHaveBeenCalled();
    expect(this.mockReader.readAsDataURL).toHaveBeenCalledWith(mockFile);
  });
});
