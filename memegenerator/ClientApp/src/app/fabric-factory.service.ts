import { Injectable } from '@angular/core';
import "fabric"
declare const fabric: any;

@Injectable({
  providedIn: 'root'
})
export class FabricFactoryService {

  private canvas: fabric.Canvas;
  private image: fabric.Image;

  constructor() { }

  createCanvas(canvas: string) {
    return this.canvas = new fabric.Canvas(canvas);
  }

  createImage(image: HTMLImageElement) {
    return this.image = new fabric.Image(image);
  }
}
