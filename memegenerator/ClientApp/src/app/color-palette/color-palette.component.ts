import { Component } from '@angular/core';
import { PrimaryColors } from './primary-colors';

@Component({
  selector: 'app-color-palette',
  templateUrl: './color-palette.component.html',
  styleUrls: ['./color-palette.component.css']
})
export class ColorPaletteComponent {

  constructor() { }

  primaryColors = PrimaryColors;

  public getTextColors() {
    return Object.keys(this.primaryColors);
  }
}
