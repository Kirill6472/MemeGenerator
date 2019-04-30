import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { LoadingAndEditImageComponent } from './loading-and-edit-image.component';
import { Component } from '@angular/core';
import { EditImageComponent } from "../edit-image/edit-image.component";

@Component({ selector: 'app-loading-image', template: '' })
class LoadingImageComponent { }

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

describe('LoadingAndEditImageComponent', () => {
  let component: LoadingAndEditImageComponent;
  let fixture: ComponentFixture<LoadingAndEditImageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        LoadingAndEditImageComponent,
        LoadingImageComponent,
        MockEditImageComponent
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadingAndEditImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display image', () => {
    const uploadedImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";

    spyOn(component.editImage, "addImageToCanvas");
    spyOn(component, "hideImageLoading");

    component.onDisplayImage(uploadedImageUrl);

    expect(component.editImage.addImageToCanvas).toHaveBeenCalledWith(uploadedImageUrl);
    expect(component.hideImageLoading).toHaveBeenCalled();
  });

  it('should hide image loading', () => {
    component.hideImageLoading();
    expect(component.showUploadedImage).toBe(true);
  });

  it('should show image loading', () => {
    const showUploadedImage = false;

    component.onShowImageLoading(showUploadedImage);
    expect(component.showUploadedImage).toBe(false);
  });
});
