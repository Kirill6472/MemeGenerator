import { Injectable } from "@angular/core";

import { fabric } from "fabric";

@Injectable({
  providedIn: "root"
})
export class FabricFactory {
  public createCanvas(canvas: string) {
    return new fabric.Canvas(canvas);
  }

  public createImage(image: HTMLImageElement) {
    return new fabric.Image(image);
  }

  public createText(sampleText: string, canvasWidth: number, textColor: string, outlineColor: string) {
    return new fabric.IText(sampleText, {
      fontFamily: "Impact",
      fontSize: 50,
      stroke: outlineColor,
      strokeWidth: 3,
      fill: textColor,
      textAlign: "center",
      left: canvasWidth / 2,
      originX: "center"
    });
  }
}
