import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { TemplateLibraryComponent } from './template-library.component';
import { MemeImage } from '../models/meme-image';
import { MemeImageService } from '../services/meme-image.service';
import { Observable } from 'rxjs';

describe('TemplateLibraryComponent', () => {
  let component: TemplateLibraryComponent;
  let fixture: ComponentFixture<TemplateLibraryComponent>;
  let memeImageServiceMock: jasmine.SpyObj<MemeImageService>;
  const response = new Observable();
  const pageSize = 9;
  const fakeMeme: MemeImage = {
    id: 1,
    name: 'fakeName',
    image: 'fakeImage',
  };

  beforeEach(async(() => {
    memeImageServiceMock = jasmine.createSpyObj('memeImageServiceMock', ['getMemes']);

    memeImageServiceMock.getMemes.and.returnValue(response);

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

  it('should load first page of memes', () => {
    const firstPage = 1;

    component.ngOnInit();

    expect(memeImageServiceMock.getMemes).toHaveBeenCalledWith(firstPage, pageSize);
  });

  it('should selected meme template', () => {
    spyOn(component.templateIsSelected, 'emit');

    component.onTemplateIsSelected(fakeMeme);

    expect(component.templateIsSelected.emit).toHaveBeenCalledWith(fakeMeme.image);
  });

  it('should load next page of memes while scrolling', () => {
    const nextPage = 2;

    component.onScroll();

    expect(memeImageServiceMock.getMemes).toHaveBeenCalledWith(nextPage, pageSize);
  });
});
