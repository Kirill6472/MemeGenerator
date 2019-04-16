import { Component } from '@angular/core';

import "fabric"
import fabric = require('fabric/fabric-impl');

@Component({
  selector: 'app-loading-image',
  templateUrl: './loading-image.component.html',
  styleUrls: ['./loading-image.component.css']
})
export class LoadingImageComponent {

  private canvas: fabric.Canvas;
  private text: fabric.Text;
  private imageInstance: any;
  uploadedImageUrl: string = '';
  condition: boolean = false;
  setMemePreview: boolean = false;
  memePreview: string = '';

  constructor() { }

  onImageIsLoaded(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.onload = (event: any) => {
        this.uploadedImageUrl = event.target.result;
        this.onAddImageToCanvas();
        this.toggle();
      }
      reader.readAsDataURL(event.target.files[0]);
    }
  }

  onAddImageToCanvas() {
    this.canvas = new fabric.Canvas('canvas');

    var image = new Image();
    image.onload = () => {
      this.imageInstance = new fabric.Image(image);
      this.canvas.setBackgroundImage(this.imageInstance, this.canvas.renderAll.bind(this.canvas), {
          scaleY: this.canvas.getHeight() / this.imageInstance.height,
          scaleX: this.canvas.getWidth() / this.imageInstance.width,
          selectable: false
      });
    };
    image.src = this.uploadedImageUrl;
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
    this.condition = false;
  }

  toggle() {
    this.condition = true;
  }

  onGenerateMeme() {
    this.setMemePreview = true;
    this.memePreview = this.canvas.toDataURL();
  }

  onCreateNewMeme() {
    this.setMemePreview = false;
    this.condition = false;
  }
}
