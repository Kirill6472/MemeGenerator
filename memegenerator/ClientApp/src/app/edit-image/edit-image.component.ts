import { Component, EventEmitter, Output, AfterViewInit } from "@angular/core";
import { FabricFactory } from "../fabric-factory/fabric-factory";
import { fabric } from "fabric";

@Component({
  selector: "app-edit-image",
  templateUrl: "./edit-image.component.html",
  styleUrls: ["./edit-image.component.css"]
})
export class EditImageComponent implements AfterViewInit {

  constructor(private fabricFactory: FabricFactory) { }

  @Output() isImageLoaded = new EventEmitter<boolean>();

  canvas: fabric.Canvas;
  private imageInstance: any;
  isToolbarShown = false;
  isMemePreview = false;

  ngAfterViewInit() {
    this.canvas = this.fabricFactory.createCanvas("canvas");
  }

  public setImage(uploadedImageUrl: string) { 
    this.showToolbar();

    const image = new Image();
    return new Promise((resolve) => {
      image.onload = () => {
        this.setBackgroundImage(image);
        resolve();
      };
      image.src = uploadedImageUrl;
    });
  }

  private setBackgroundImage(image: HTMLImageElement) {
    this.imageInstance = this.fabricFactory.createImage(image);
    this.canvas.setBackgroundImage(this.imageInstance, this.canvas.renderAll.bind(this.canvas), {
      scaleY: this.canvas.getHeight() / this.imageInstance.height,
      scaleX: this.canvas.getWidth() / this.imageInstance.width,
      selectable: false
    }); 
  }

  private showToolbar() {
    this.isToolbarShown = true;
  }

  private showMemePreview() {
    this.isMemePreview = true;
  }

  private generateMeme() {
    return this.canvas.toDataURL();
  }

  public generateAndPreviewMeme() {
    this.generateMeme();
    this.showMemePreview();
  }

  public createNewMeme() {
    this.clearCanvas();

    this.isMemePreview = false;
    this.isToolbarShown = false;

    this.isImageLoaded.emit(false);
  }

  private clearCanvas() {
    this.canvas.clear();
  }
}
