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
  showToolbar = false;
  displayMemePreview = false;
  ctx = null;
  
  constructor(private fabricFactory: FabricFactoryService) { }

  ngAfterViewInit() {
    this.canvas = this.fabricFactory.createCanvas("canvas");
  }

  public addImageToCanvas(uploadedImageUrl: string) { 
    this.showToolbarAndImage();

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

  public onAddText() {
    this.text = this.fabricFactory.createText("Sample\ntext");
    this.canvas.add(this.text);
  }

  public onDeleteText() {
    this.canvas.remove(this.canvas.getActiveObject());
  }

  public showToolbarAndImage() {
    this.showToolbar = true;
  }

  public showMemePreview() {
    this.displayMemePreview = true;
  }

  public generateMeme() {
    return this.canvas.toDataURL();
  }

  public onGenerateAndDisplayMeme() {
    this.showMemePreview();
    this.generateMeme();
  }

  public onCreateNewMeme() {
    this.canvas.clear();
    this.displayMemePreview = false;
    this.showToolbar = false;
    this.imageLoaded.emit(false);
  }
}
