import { Component, OnInit } from '@angular/core';

import "fabric"

@Component({
  selector: 'app-loading-image',
  templateUrl: './loading-image.component.html',
  styleUrls: ['./loading-image.component.css']
})
export class LoadingImageComponent implements OnInit {

  private canvas;
  private text;
  private imageInstance;
  uploadedImageUrl = '';

  constructor() { }

  onImageIsLoaded(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.onload = (event: any) => {
        this.uploadedImageUrl = event.target.result;        
      }

      reader.readAsDataURL(event.target.files[0]);
    }
  }

  onCreateCanvas() {
    this.canvas = new fabric.Canvas('canvas');

    var myImage = new Image();
    myImage.src = this.uploadedImageUrl;

    this.imageInstance = new fabric.Image(myImage);

    this.text = new fabric.Text('Sample text', {
      fontFamily: 'Impact',
      fontSize: 50,
      stroke: '#000000',
      strokeWidth: 3,
      fill: '#ffffff'
    });

    this.canvas.add(this.text);
    this.canvas.setBackgroundImage(this.imageInstance, this.canvas.renderAll.bind(this.canvas), {
        scaleY: this.canvas.height / this.imageInstance.width,
        scaleX: this.canvas.width / this.imageInstance.width,
        selectable: false
    });
  }
}
