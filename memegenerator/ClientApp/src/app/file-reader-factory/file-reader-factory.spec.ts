import { TestBed, inject } from '@angular/core/testing';
import { FileReaderFactory } from "./file-reader-factory";

describe('FileReaderFactory', () => {
  let loadingImageFactoryService: FileReaderFactory;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FileReaderFactory]
    });
  });

  it('should be created', inject([FileReaderFactory], (service: FileReaderFactory) => {
    expect(service).toBeTruthy();
  }));

  it('should create file reader', () => {
    loadingImageFactoryService = new FileReaderFactory();

    expect(loadingImageFactoryService.createFileReader()).not.toBe(null);
  });
});
