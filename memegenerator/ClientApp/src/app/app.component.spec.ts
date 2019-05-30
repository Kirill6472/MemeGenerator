import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { MockNavMenuComponent } from "./nav-menu/nav-menu-mock.component";
import { MockLoadingImageComponent } from "./loading-image/loading-image-mock.component";
import { MockEditImageComponent } from "./edit-image/edit-image-mock.component";
import { MockMemeViewerComponent } from './meme-viewer/meme-viewer-mock.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        MockNavMenuComponent,
        MockLoadingImageComponent,
        MockEditImageComponent,
        MockMemeViewerComponent
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it(`should have as title 'Meme Generator'`, () => {
    expect(component.title).toEqual('Meme Generator');
  });

  it('should display image', () => {
    const uploadedImageUrl =
      "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";

    spyOn(component.editImageComponent, "setImage");

    component.updateImage(uploadedImageUrl);

    expect(component.editImageComponent.setImage).toHaveBeenCalledWith(uploadedImageUrl);
    expect(component.isUploadedImageShown).toBe(true);
    expect(component.isMemeEdited).toBe(true);
  });

  it('should set meme URL', () => {
    const mockUrl =
      "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==";
    component.isMemeEdited = true;

    component.setMemeUrl(mockUrl);

    expect(component.generatedMemeUrl).toContain(mockUrl);
    expect(component.isMemeDisplayed).toBe(true);
    expect(component.isMemeEdited).toBe(false);
  });

  it('should hide meme viewer and show loading image', () => {
    component.isMemeDisplayed = true;

    component.onCreateNewMeme();

    expect(component.isMemeDisplayed).toBe(false);
    expect(component.isUploadedImageShown).toBe(true);
  });
});
