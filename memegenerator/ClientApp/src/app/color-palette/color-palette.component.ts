import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-color-palette',
  templateUrl: './color-palette.component.html',
  styleUrls: ['./color-palette.component.css']
})
export class ColorPaletteComponent {

  @Input() availableColors: string[];
  @Input() selectedColor: string;
  @Output() selectedColorChange = new EventEmitter<string>();

  setColor(event) {
    this.selectedColorChange.emit(event.target.value);
  }
}
