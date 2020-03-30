import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { MemeImageService } from './meme-image.service';
import { MemeImage } from '../models/meme-image';

describe('MemeImageService', () => {
  let httpMock: HttpTestingController;
  let memeImageService: MemeImageService;
  const fakeMemes: MemeImage[] = [{
    id: 1,
    name: 'fakeName',
    image: 'fakeImage'
  }];
  const fakeMemesUrl = '/api/memes';
  const fakePage = 1;
  const fakePageSize = 9;
  
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MemeImageService]
    });

    memeImageService = TestBed.get(MemeImageService);
    httpMock = TestBed.get(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([MemeImageService], (service: MemeImageService) => {
    expect(service).toBeTruthy();
  }));

  it('should get memes', () => {
    memeImageService.getMemes(fakePage, fakePageSize).subscribe((result) => {
      expect(result).toEqual(fakeMemes);
    });

    const req = httpMock.expectOne(`${fakeMemesUrl}/${fakePage}/${fakePageSize}`);
    expect(req.request.method).toEqual("GET");
    req.flush(fakeMemes);
  });
});
