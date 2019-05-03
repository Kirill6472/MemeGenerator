import { Injectable } from "@angular/core";
import "fabric"
declare const fabric: any;

@Injectable({
  providedIn: "root"
})
export class FabricFactoryService {

  private canvas: fabric.Canvas;

  public createCanvas(canvas: string) {
    return this.canvas = new fabric.Canvas(canvas);
  }

  public createImage(image: HTMLImageElement) {
    return new fabric.Image(image);
  }

  public createText(sampleText: string) {
    return new fabric.IText(sampleText, {
      fontFamily: "Impact",
      fontSize: 50,
      stroke: "#000000",
      strokeWidth: 3,
      fill: "#ffffff",
      textAlign: "center",
      left: this.canvas.getWidth() / 2,
      originX: "center"
    });
  }
}
