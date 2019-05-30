import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { FabricFactory } from "../fabric-factory/fabric-factory";
import { fabric } from "fabric";

@Component({
  selector: "app-edit-image",
  templateUrl: "./edit-image.component.html",
  styleUrls: ["./edit-image.component.css"]
})
export class EditImageComponent implements OnInit {

  constructor(private fabricFactory: FabricFactory) { }

  @Output() generatedMemeUrl = new EventEmitter<string>(); 

  public canvas: fabric.Canvas;
  private imageInstance: any;

  ngOnInit() {
    this.canvas = this.fabricFactory.createCanvas("canvas");
  }

  public setImage(uploadedImageUrl: string) { 
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

  public generateMeme() {
    this.generatedMemeUrl.emit(this.canvas.toDataURL());
    this.clearCanvas();
  }

  private clearCanvas() {
    this.canvas.clear();
  }
}
