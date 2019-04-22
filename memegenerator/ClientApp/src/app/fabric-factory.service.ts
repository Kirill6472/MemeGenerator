import { Injectable } from '@angular/core';
import "fabric"
declare const fabric: any;

@Injectable({
  providedIn: 'root'
})
export class FabricFactoryService {

  private canvas: fabric.Canvas;

  constructor() { }

  createCanvas(canvas: string) {
    return this.canvas = new fabric.Canvas(canvas);
  }

  createImage() {
    return new Image();
  }
}
