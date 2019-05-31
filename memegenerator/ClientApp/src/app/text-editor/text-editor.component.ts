import { Component, Input } from '@angular/core';
import { FabricFactory } from '../fabric-factory/fabric-factory';
import { PrimaryColors } from '../edit-image/primary-colors';
import { fabric } from "fabric";

@Component({
  selector: 'app-text-editor',
  templateUrl: './text-editor.component.html',
  styleUrls: ['./text-editor.component.css']
})
export class TextEditorComponent {

  constructor(private fabricFactory: FabricFactory) { }

  @Input('canvas') canvas: fabric.Canvas;

  public textColor = PrimaryColors.white;
  public outlineColor = PrimaryColors.black;
  public primaryColors = Object.keys(PrimaryColors).map(i => PrimaryColors[i]);

  public addText() {
    this.canvas.add(this.fabricFactory.createText(
      "Sample\ntext",
      this.canvas.getWidth(),
      this.textColor,
      this.outlineColor
    ));
  }

  public deleteSelectedText() {
    this.canvas.remove(this.canvas.getActiveObject());
  }

  public textColorChange() {
    if (this.isSelectedObjectAText()) {
      this.canvas.getActiveObject().setColor(this.textColor);
      this.canvas.renderAll();
    } 
  }

  public outlineColorChange() {
    if (this.isSelectedObjectAText()) {
      this.canvas.getActiveObject().set("stroke", this.outlineColor);
      this.canvas.renderAll();
    }
  }

  private isSelectedObjectAText(): boolean {
    return ((this.canvas.getActiveObject() != null) &&
            (this.canvas.getActiveObject().type === 'i-text'));
  }
}
