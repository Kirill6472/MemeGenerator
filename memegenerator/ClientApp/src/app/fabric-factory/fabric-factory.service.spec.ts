import { TestBed, inject } from '@angular/core/testing';
import { FabricFactoryService } from './fabric-factory.service';

describe('FabricFactoryService', () => {
  let fabricFactoryService: FabricFactoryService;

  beforeEach(() => {
    fabricFactoryService = new FabricFactoryService();

    TestBed.configureTestingModule({
      providers: [FabricFactoryService]
    });
  });

  it('should be created', inject([FabricFactoryService], (service: FabricFactoryService) => {
    expect(service).toBeTruthy();
  }));

  it('should return new image', () => {
    const fakeImage = new Image();

    expect(fabricFactoryService.createImage(fakeImage)).not.toBe(null);
  });

  it('should return new text', () => {
    const fakeText = "Sample text";
    const fakeCanvasWidth = 600;

    expect(fabricFactoryService.createText(fakeText, fakeCanvasWidth)).not.toBe(null);
  });
});
