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
  private image: HTMLImageElement;
  private imageInstance: HTMLImageElement;
  private text: fabric.Text;
  uploadedImageUrl: string = '';
  private imageUploaded: boolean = false;
  private setMemePreview: boolean = false;
  private memePreview: string = '';
  
  constructor(private fabricFactory: FabricFactoryService) { }

  addImageToCanvas() {
    this.canvas = this.fabricFactory.createCanvas('canvas');
    this.image = this.fabricFactory.createImage();
    
    return new Promise((resolve) => {
      this.image.onload = () => {
        this.imageInstance = new fabric.Image(this.image);
        this.canvas.setBackgroundImage(this.imageInstance, this.canvas.renderAll.bind(this.canvas), {
          scaleY: this.canvas.getHeight() / this.imageInstance.height,
          scaleX: this.canvas.getWidth() / this.imageInstance.width,
          selectable: false
        });
        resolve();
      };
      this.image.src = this.uploadedImageUrl;
    });
  }

  onAddText() {
    var sampleText = 'Sample\ntext';
    this.text = new fabric.IText(sampleText, {
      fontFamily: 'Impact',
      fontSize: 50,
      stroke: '#000000',
      strokeWidth: 3,
      fill: '#ffffff',
      textAlign: 'center',
      left: this.canvas.getWidth() / 2,
      originX: 'center'
    });

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
