import { TestBed, inject } from '@angular/core/testing';

import { MemeImageService } from './meme-image.service';

describe('MemeImageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MemeImageService]
    });
  });

  it('should be created', inject([MemeImageService], (service: MemeImageService) => {
    expect(service).toBeTruthy();
  }));
});
