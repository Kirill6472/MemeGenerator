import { Component, Output, EventEmitter } from '@angular/core';
import { PrimaryColors } from './primary-colors';

@Component({
  selector: 'app-color-palette',
  templateUrl: './color-palette.component.html',
  styleUrls: ['./color-palette.component.css']
})
export class ColorPaletteComponent {

  @Output() textColorChange = new EventEmitter<string>();

  setTextColor(event) {
    this.textColorChange.emit(event.target.value);
  }

  primaryColors = PrimaryColors;

  public getTextColors() {
    return Object.keys(this.primaryColors);
  }
}
