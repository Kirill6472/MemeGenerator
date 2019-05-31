import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-meme-viewer',
  templateUrl: './meme-viewer.component.html',
  styleUrls: ['./meme-viewer.component.css']
})
export class MemeViewerComponent {

  @Input() memeUrl: string;
}
