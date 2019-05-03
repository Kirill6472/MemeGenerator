import { Component, EventEmitter, Output, AfterViewInit } from "@angular/core";
import { FabricFactoryService } from "../fabric-factory.service";

import "fabric";
declare const fabric: any;

@Component({
  selector: "app-edit-image",
  templateUrl: "./edit-image.component.html",
  styleUrls: ["./edit-image.component.css"]
})
export class EditImageComponent implements AfterViewInit {
  @Output() imageLoaded = new EventEmitter<boolean>();

  canvas: fabric.Canvas;
  imageInstance: any;
  text: fabric.Text;
  isToolbarShown = false;
  isMemePreview = false;

  constructor(private fabricFactory: FabricFactoryService) { }

  ngAfterViewInit() {
    this.canvas = this.fabricFactory.createCanvas("canvas");
  }

  public addImageToCanvas(uploadedImageUrl: string) { 
    this.showToolbar();

    const image = new Image();
    return new Promise((resolve) => {
      image.onload = () => {
        this.imageInstance = this.fabricFactory.createImage(image);
        this.canvas.setBackgroundImage(this.imageInstance, this.canvas.renderAll.bind(this.canvas), {
          scaleY: this.canvas.getHeight() / this.imageInstance.height,
          scaleX: this.canvas.getWidth() / this.imageInstance.width,
          selectable: false
        }); 
        resolve();
      };
      image.src = uploadedImageUrl;
    });
  }

  public addText() {
    this.text = this.fabricFactory.createText("Sample\ntext");
    this.canvas.add(this.text);
  }

  public deleteSelectedText() {
    this.canvas.remove(this.canvas.getActiveObject());
  }

  public showToolbar() {
    this.isToolbarShown = true;
  }

  public showMemePreview() {
    this.isMemePreview = true;
  }

  public generateMeme() {
    return this.canvas.toDataURL();
  }

  public generateAndDisplayMeme() {
    this.generateMeme();
    this.showMemePreview();
  }

  public createNewMeme() {
    this.canvas.clear();

    this.isMemePreview = false;
    this.isToolbarShown = false;

    this.imageLoaded.emit(false);
  }
}
