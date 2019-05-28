import { Component, Input } from '@angular/core';
import fabric = require('fabric/fabric-impl');
import { FabricFactory } from '../fabric-factory/fabric-factory';
import { PrimaryColors } from '../edit-image/primary-colors';

@Component({
  selector: 'app-text-editor',
  templateUrl: './text-editor.component.html',
  styleUrls: ['./text-editor.component.css']
})
export class TextEditorComponent {

  constructor(private fabricFactory: FabricFactory) { }

  @Input('canvas') canvas: fabric.Canvas;

  textColor = PrimaryColors.white;
  outlineColor = PrimaryColors.black;
  primaryColors = Object.keys(PrimaryColors).map(i => PrimaryColors[i]);

  public addText() {
    this.canvas.add(this.fabricFactory.createText("Sample\ntext", this.canvas.getWidth(), this.textColor, this.outlineColor));
  }

  public deleteSelectedText() {
    this.canvas.remove(this.canvas.getActiveObject());
  }

  public textColorChange(color) {
    this.textColor = color;

    if (this.canvas.getActiveObject()) {
      this.canvas.getActiveObject().setColor(this.textColor);
    }

    this.canvas.renderAll();
  }

  public outlineColorChange(color) {
    this.outlineColor = color;

    if (this.canvas.getActiveObject()) {
      this.canvas.getActiveObject().set("stroke", this.outlineColor);
    }

    this.canvas.renderAll();
  }
}
