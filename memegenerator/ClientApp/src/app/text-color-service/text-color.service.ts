import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TextColorService {
  textColor: string;
  textColorChange: BehaviorSubject<string> = new BehaviorSubject<string>("#FFFFFF");

  textOutlineColor: string;
  textOutlineColorChange: BehaviorSubject<string> = new BehaviorSubject<string>("#000000");

  constructor() {
    this.textColorChange.subscribe((value) => {
      this.textColor = value
    });

    this.textOutlineColorChange.subscribe((value) => {
      this.textOutlineColor = value
    });
  }

  updateTextColor(textColor: string) {
    this.textColor = textColor;
  }

  updateTextOutlineColor(textOutlineColor: string) {
    this.textOutlineColor = textOutlineColor;
  }
}
