import { Component, Input } from "@angular/core";

@Component({
  selector: 'app-color-palette',
  template: ''
})
export class ColorPaletteMockComponent {

  @Input() availableColors: string[];
  @Input() selectedColor: string;
}
