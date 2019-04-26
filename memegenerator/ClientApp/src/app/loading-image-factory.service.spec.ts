import { TestBed, inject } from '@angular/core/testing';

import { LoadingImageFactoryService } from './loading-image-factory.service';

describe('LoadingImageFactoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoadingImageFactoryService]
    });
  });

  it('should be created', inject([LoadingImageFactoryService], (service: LoadingImageFactoryService) => {
    expect(service).toBeTruthy();
  }));
});
