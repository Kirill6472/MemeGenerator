import { Component } from '@angular/core';
import { PrimaryColors } from './primary-colors';
import { TextColorService } from '../text-color-service/text-color.service';
import { EditImageComponent } from '../edit-image/edit-image.component';

@Component({
  selector: 'app-color-palette',
  templateUrl: './color-palette.component.html',
  styleUrls: ['./color-palette.component.css']
})
export class ColorPaletteComponent {

  constructor(private textColorService: TextColorService, private editImageComponent: EditImageComponent) { }

  primaryColors = PrimaryColors;

  get textColor(): string {
    return this.textColorService.textColor;
  }

  public onChangeTextColor(event) {
    this.textColorService.updateTextColor(event.target.value);
    this.editImageComponent.changeTextColor();
  }

  public onChangeOutlineColor(event) {
    this.textColorService.updateTextOutlineColor(event.target.value);
    this.editImageComponent.changeTextOutlineColor();
  }

  public getTextColors() {
    return Object.keys(this.primaryColors);
  }
}
