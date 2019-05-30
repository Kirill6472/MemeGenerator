import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-meme-viewer',
  templateUrl: './meme-viewer.component.html',
  styleUrls: ['./meme-viewer.component.css']
})
export class MemeViewerComponent {

  constructor() { }

  @Input() memeUrl: string;
  @Output() onCreateNewMeme = new EventEmitter();

  createNewMeme() {
    this.onCreateNewMeme.emit();
  }
}
