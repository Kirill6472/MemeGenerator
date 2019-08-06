import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MemeImageService } from './meme-image.service';

describe('MemeImageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MemeImageService]
    });
  });

  it('should be created', inject([MemeImageService], (service: MemeImageService) => {
    expect(service).toBeTruthy();
  }));
});
