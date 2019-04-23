import { Component } from '@angular/core';
import { FabricFactoryService } from '../fabric-factory.service';

import "fabric";
declare const fabric: any;

@Component({
  selector: 'app-edit-image',
  templateUrl: './edit-image.component.html',
  styleUrls: ['./edit-image.component.css']
})
export class EditImageComponent {

  private canvas: fabric.Canvas;
  private imageInstance: any;
  private text: fabric.Text;
  uploadedImageUrl: string = '';
  private imageUploaded: boolean = false;
  private setMemePreview: boolean = false;
  private memePreview: string = '';
  
  constructor(private fabricFactory: FabricFactoryService) { }

  addImageToCanvas() {
    this.canvas = this.fabricFactory.createCanvas('canvas');

    var image = new Image();
    
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
      image.src = this.uploadedImageUrl;
    });
  }

  onAddText() {
    let sampleText = 'Sample\ntext';

    this.text = this.fabricFactory.createText(sampleText);
    this.canvas.add(this.text);
  }

  onDeleteText() {
    this.canvas.remove(this.canvas.getActiveObject());
  }

  onClearCanvas() {
    this.canvas.clear();
    this.imageUploaded = false;
  }

  hideImageLoading() {
    this.imageUploaded = true;
  }

  onGenerateMeme() {
    this.setMemePreview = true;
    this.memePreview = this.canvas.toDataURL();
  }

  onCreateNewMeme() {
    this.setMemePreview = false;
    this.imageUploaded = false;
  }
}
