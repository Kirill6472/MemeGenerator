import { Component, OnInit } from '@angular/core';

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
  private imageInstance: fabric.Image;
  uploadedImageUrl = '';

  constructor() { }

  onImageIsLoaded(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.onload = (event: any) => {
        this.uploadedImageUrl = event.target.result;
        this.onCreateCanvas();

      }
      reader.readAsDataURL(event.target.files[0]);  
    }
  }

  onCreateCanvas() {
    this.canvas = new fabric.Canvas('canvas');

    var sampleText = 'Sample\ntext';
    this.text = new fabric.Text(sampleText, {
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

    var image = new Image();
    image.onload = () => {
      this.imageInstance = new fabric.Image(image);
      this.canvas.setBackgroundImage(this.imageInstance, this.canvas.renderAll.bind(this.canvas), {
          scaleY: this.canvas.getHeight() / this.imageInstance.width,
          scaleX: this.canvas.getWidth() / this.imageInstance.width,
          selectable: false
      });
    };
    image.src = this.uploadedImageUrl;
  }
}
