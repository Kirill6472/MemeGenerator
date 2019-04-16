import { Component, Input } from '@angular/core';

import fabric = require('fabric/fabric-impl');

@Component({
  selector: 'app-edit-image',
  templateUrl: './edit-image.component.html',
  styleUrls: ['./edit-image.component.css']
})
export class EditImageComponent {

  private canvas: fabric.Canvas;
  private text: fabric.Text;

  constructor() { }

  onAddTextToCanvas() {
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
  }
}
