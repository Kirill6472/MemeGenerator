import { Component, EventEmitter, Output } from "@angular/core";
import { FabricFactoryService } from "../fabric-factory.service";

import "fabric";
declare const fabric: any;

@Component({
  selector: "app-edit-image",
  templateUrl: "./edit-image.component.html",
  styleUrls: ["./edit-image.component.css"]
})
export class EditImageComponent {
  @Output() imageLoading = new EventEmitter<boolean>();
  showUploadImage: true;

  canvas: fabric.Canvas;
  imageInstance: any;
  text: fabric.Text;
  showToolbar = false;
  displayMemePreview = false;
  
  constructor(private fabricFactory: FabricFactoryService) { }

  getCanvas() {
    this.canvas = this.fabricFactory.createCanvas("canvas");
  }

  addImageToCanvas(uploadedImageUrl: string) {
    this.getCanvas();
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

  onAddText() {
    this.text = this.fabricFactory.createText("Sample\ntext");
    this.canvas.add(this.text);
  }

  onDeleteText() {
    this.canvas.remove(this.canvas.getActiveObject());
  }

  showToolbarAndImage() {
    this.showToolbar = true;
  }

  showMemePreview() {
    this.displayMemePreview = true;
  }

  generateMeme() {
    return this.canvas.toDataURL();
  }

  onGenerateAndDisplayMeme() {
    this.showMemePreview();
    this.generateMeme();
  }

  onCreateNewMeme() {
    this.displayMemePreview = false;
    this.showToolbar = false;
    this.imageLoading.emit(false);
  }
}
