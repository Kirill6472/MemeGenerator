import { TestBed, inject } from '@angular/core/testing';
import { FileReaderFactoryService } from "./file-reader-factory.service";

describe('LoadingImageFactoryService', () => {
  let loadingImageFactoryService: FileReaderFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FileReaderFactoryService]
    });
  });

  it('should be created', inject([FileReaderFactoryService], (service: FileReaderFactoryService) => {
    expect(service).toBeTruthy();
  }));

  it('should create file reader', () => {
    loadingImageFactoryService = new FileReaderFactoryService();

    expect(loadingImageFactoryService.createFileReader()).not.toBe(null);
  });
});
