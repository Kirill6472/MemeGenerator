import { Component, Input } from "@angular/core";

@Component({
  selector: 'app-meme-viewer',
  template: ''
})
export class MockMemeViewerComponent {
  @Input() memeUrl: string;
}
