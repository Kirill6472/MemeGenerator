import { Injectable } from "@angular/core";
import { fabric } from "fabric";

@Injectable({
  providedIn: "root"
})
export class FabricFactoryService {
  public createCanvas(canvas: string) {
    return new fabric.Canvas(canvas);
  }

  public createImage(image: HTMLImageElement) {
    return new fabric.Image(image);
  }

  public createText(sampleText: string, canvasWidth: number) {
    return new fabric.IText(sampleText, {
      fontFamily: "Impact",
      fontSize: 50,
      stroke: "#000000",
      strokeWidth: 3,
      fill: "#ffffff",
      textAlign: "center",
      left: canvasWidth / 2,
      originX: "center"
    });
  }
}
