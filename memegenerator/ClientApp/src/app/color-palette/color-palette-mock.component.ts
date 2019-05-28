import { Component, Input } from "@angular/core";

@Component({
  selector: 'app-color-palette',
  template: ''
})
export class ColorPaletteMockComponent {

  @Input() defaultColor: string;
  @Input() availableColors: string[];
}
