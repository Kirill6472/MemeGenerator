import { Component, EventEmitter, Output, AfterViewInit } from "@angular/core";
import { FabricFactory } from "../fabric-factory/fabric-factory";
import { TextCommandFactory } from "../text-command/text-command-factory";
import { fabric } from "fabric";
import { TextColorService } from "../text-color-service/text-color.service";

@Component({
  selector: "app-edit-image",
  templateUrl: "./edit-image.component.html",
  styleUrls: ["./edit-image.component.css"]
})
export class EditImageComponent implements AfterViewInit {

  constructor(private fabricFactory: FabricFactory, private textCommandFactory: TextCommandFactory, private textColorService: TextColorService) { }

  @Output() isImageLoaded = new EventEmitter<boolean>();

  canvas: fabric.Canvas;
  private imageInstance: any;
  isToolbarShown = false;
  isMemePreview = false;
  textCommand: ICommand;

  ngAfterViewInit() {
    this.canvas = this.fabricFactory.createCanvas("canvas");
    this.textCommand = this.textCommandFactory.createTextCommand(this);
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

  public addText() {
    this.textCommand.execute();
  }

  public deleteSelectedText() {
    this.canvas.remove(this.canvas.getActiveObject());
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

  get textColor(): string {
    return this.textColorService.textColor;
  }

  get textOutlineColor(): string {
    return this.textColorService.textOutlineColor;
  }

  public changeTextColor() {
    if (this.canvas.getActiveObject()) {
      this.canvas.getActiveObject().setColor(this.textColor);
    }

    this.canvas.renderAll();
  }

  public changeTextOutlineColor() {
    if (this.canvas.getActiveObject()) {
      this.canvas.getActiveObject().set("stroke", this.textOutlineColor);
    }
    this.canvas.renderAll();
  }
}
