import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-color-palette',
  templateUrl: './color-palette.component.html',
  styleUrls: ['./color-palette.component.css']
})
export class ColorPaletteComponent {

  @Input() defaultColor: string;
  @Input() availableColors: string[];
  @Output() colorChange = new EventEmitter<string>();

  setColor(event) {
    this.colorChange.emit(event.target.value);
  }
}
