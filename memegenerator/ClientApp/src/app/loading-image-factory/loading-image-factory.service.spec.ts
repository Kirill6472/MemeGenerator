import { TestBed, inject } from '@angular/core/testing';
import { LoadingImageFactoryService } from './loading-image-factory.service';

describe('LoadingImageFactoryService', () => {
  let loadingImageFactoryService: LoadingImageFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoadingImageFactoryService]
    });
  });

  it('should be created', inject([LoadingImageFactoryService], (service: LoadingImageFactoryService) => {
    expect(service).toBeTruthy();
  }));

  it('should create file reader', () => {
    loadingImageFactoryService = new LoadingImageFactoryService();

    expect(loadingImageFactoryService.createFileReader()).not.toBe(null);
  });
});
