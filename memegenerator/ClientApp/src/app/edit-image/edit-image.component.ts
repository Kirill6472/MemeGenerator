import { Component, Input } from '@angular/core';

import fabric = require('fabric/fabric-impl');

@Component({
  selector: 'app-edit-image',
  templateUrl: './edit-image.component.html',
  styleUrls: ['./edit-image.component.css']
})
export class EditImageComponent {

  private _canvas: fabric.Canvas;

  @Input()
  set canvas(canvas: fabric.Canvas) {
    this._canvas = canvas;
  }

  get canvas(): fabric.Canvas { return this._canvas; }

  private text: fabric.Text;

  constructor() { }

  addTextToCanvas() { this.addText(); }

  private addText() {
    var sampleText = 'Sample\ntext';
    this.text = new fabric.IText(sampleText, {
      fontFamily: 'Impact',
      fontSize: 50,
      stroke: '#000000',
      strokeWidth: 3,
      fill: '#ffffff',
      textAlign: 'center',
      left: this._canvas.getWidth() / 2,
      originX: 'center'
    });

    this._canvas.add(this.text);
  }
}
