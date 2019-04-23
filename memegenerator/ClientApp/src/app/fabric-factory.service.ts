import { Injectable } from '@angular/core';
import "fabric"
declare const fabric: any;

@Injectable({
  providedIn: 'root'
})
export class FabricFactoryService {

  private canvas: fabric.Canvas;
  private image: fabric.Image;
  private text: fabric.IText;

  constructor() { }

  createCanvas(canvas: string) {
    return this.canvas = new fabric.Canvas(canvas);
  }

  createImage(image: HTMLImageElement) {
    return this.image = new fabric.Image(image);
  }

  createText(sampleText: string) {
    return this.text = new fabric.IText(sampleText, {
      fontFamily: 'Impact',
      fontSize: 50,
      stroke: '#000000',
      strokeWidth: 3,
      fill: '#ffffff',
      textAlign: 'center',
      left: this.canvas.getWidth() / 2,
      originX: 'center'
    });
  }
}
