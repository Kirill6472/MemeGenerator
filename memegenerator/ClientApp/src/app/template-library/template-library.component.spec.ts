import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { TemplateLibraryComponent } from './template-library.component';
import { MemeImage } from '../models/meme-image';
import { MemeImageService } from '../services/meme-image.service';

describe('TemplateLibraryComponent', () => {
  let component: TemplateLibraryComponent;
  let fixture: ComponentFixture<TemplateLibraryComponent>;
  let memeImageServiceMock: jasmine.SpyObj<MemeImageService>;
  const response: MemeImage[] = [];
  let fakeEvent;

  beforeEach(async(() => {
    memeImageServiceMock = jasmine.createSpyObj('memeImageServiceMock', ['getMemes']);

    fakeEvent = {
      target: {
        src: {}
      }
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [TemplateLibraryComponent],
      providers: [
        { provide: MemeImageService, useValue: memeImageServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TemplateLibraryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load memes', () => {
    memeImageServiceMock.getMemes.and.returnValue(response);

    component.ngOnInit();

    expect(component.memes).toEqual(response);
  });

  it('should selected template', () => {
    spyOn(component.templateIsSelected, 'emit');

    component.onTemplateIsSelected(fakeEvent);

    expect(component.templateIsSelected.emit).toHaveBeenCalledWith(fakeEvent.target.src);
  });

  it('should load memes while scrolling', () => {
    memeImageServiceMock.getMemes.and.returnValue(response);

    component.onScroll();

    expect(component.memes).toEqual(response);
  });
});
