import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { LoadingImageComponent } from './loading-image.component';
import { EditImageComponent } from '../edit-image/edit-image.component';

describe('LoadingImageComponent', () => {
  let component: LoadingImageComponent;
  let fixture: ComponentFixture<LoadingImageComponent>;
  let editImageComponentMock: jasmine.SpyObj<EditImageComponent>;

  beforeEach(async(() => {
    editImageComponentMock = jasmine.createSpyObj('editImageComponentMock', ['addImageToCanvas', 'hideImageLoading']);

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

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render input tag with type file', () => {
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('input'));
  });

  it('should load image and add to canvas', () => {
    //component.onImageIsLoaded();
    
    //expect(editImageComponentMock.addImageToCanvas).toHaveBeenCalled();
  });

  it('should hide image loading element', () => {
    //spyOn(editImageComponentStub, 'hideImageLoading');
    //editImageComponentStub.hideImageLoading();
    //fixture.detectChanges();
    //expect(editImageComponentStub.hideImageLoading).toHaveBeenCalled();
  });
});
