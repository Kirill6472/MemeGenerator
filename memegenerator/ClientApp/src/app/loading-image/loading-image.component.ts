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
  private imgElement;
  private imgInstance;
  private img;
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
    this.onCreateCanvas();
  }

  onCreateCanvas() {
    this.canvas = new fabric.Canvas('canvas');

    this.imgElement = document.getElementById('image');
    this.imgInstance = new fabric.Image(this.imgElement, {
    });

    this.text = new fabric.Text('Sample text', {
      fontFamily: 'Impact',
      fontSize: 50,
      stroke: '#000000',
      strokeWidth: 3,
      fill: '#ffffff'
    });

    this.canvas.add(this.imgInstance);
    this.canvas.add(this.text);
  }
}
