import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ImageEditorComponent } from "./image-editor.component";
import { MockLoadingImageComponent } from "../loading-image/MockLoadingImageComponent";
import { MockEditImageComponent } from "../edit-image/MockEditImageComponent";

describe('ImageEditorComponent', () => {
  let component: ImageEditorComponent;
  let fixture: ComponentFixture<ImageEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ImageEditorComponent,
        MockLoadingImageComponent,
        MockEditImageComponent
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ImageEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display image', () => {
    const uploadedImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";

    spyOn(component.editImage, "addImageToCanvas");

    component.displayImage(uploadedImageUrl);

    expect(component.editImage.addImageToCanvas).toHaveBeenCalledWith(uploadedImageUrl);
    expect(component.showUploadedImage).toBe(true);
  });

  it('should show image loading', () => {
    const showUploadedImage = false;

    component.showImageLoading(showUploadedImage);

    expect(component.showUploadedImage).toBe(false);
  });
});
