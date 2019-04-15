import { Component, OnInit } from '@angular/core';
import "fabric"

@Component({
  selector: 'app-edit-image',
  templateUrl: './edit-image.component.html',
  styleUrls: ['./edit-image.component.css']
})
export class EditImageComponent implements OnInit {

  private canvas;
  private text;

  constructor() { }

  ngOnInit() {
    this.canvas = new fabric.Canvas('canvas', { });

    this.text = new fabric.Text('Sample text', {
      fontFamily: 'Impact',
      fontSize: 50,
      stroke: '#000000',
      strokeWidth: 3,
      fill: '#ffffff'
    });

    this.canvas.add(this.text);
  }
}
