import { TestBed, inject } from '@angular/core/testing';
import { FabricFactory } from './fabric-factory';

describe('FabricFactory', () => {
  let fabricFactory: FabricFactory;

  beforeEach(() => {
    fabricFactory = new FabricFactory();

    TestBed.configureTestingModule({
      providers: [FabricFactory]
    });
  });

  it('should be created', inject([FabricFactory], (service: FabricFactory) => {
    expect(service).toBeTruthy();
  }));

  it('should return new canvas', () => {
    let mockCanvas = "canvas";

    expect(fabricFactory.createCanvas(mockCanvas)).not.toBe(null);
  });

  it('should return new image', () => {
    const fakeImage = new Image();

    expect(fabricFactory.createImage(fakeImage)).not.toBe(null);
  });

  it('should return new text', () => {
    const fakeText = "Sample text";
    const fakeCanvasWidth = 600;
    const fakeTextColor = "#FFFFFF";
    const fakeOutlineColor = "#000000";

    expect(fabricFactory.createText(fakeText, fakeCanvasWidth, fakeTextColor, fakeOutlineColor)).not.toBe(null);
  });
});
