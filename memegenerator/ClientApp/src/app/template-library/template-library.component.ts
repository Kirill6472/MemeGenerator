import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MemeImageService } from '../services/meme-image.service';
import { MemeImage } from '../models/meme-image';

@Component({
  selector: 'app-template-library',
  templateUrl: './template-library.component.html',
  styleUrls: ['./template-library.component.css'],
  providers: [MemeImageService]
})
export class TemplateLibraryComponent implements OnInit {

  public memes: MemeImage[];
  public memeNum = 8;

  @Output() templateIsSelected = new EventEmitter<string>();
  
  constructor(private memeImageService: MemeImageService) { }

  ngOnInit() {
    this.loadMemes(this.memeNum);
  }

  private loadMemes(memeNum) {
    this.memeImageService.getMeme(memeNum).subscribe((data: MemeImage) => this.memes = this.memes.concat(data));
  }

  public onTemplateIsSelected(event: any) {
    this.templateIsSelected.emit(event.target.src);
  }

  onScroll() {
    this.memeNum++;
    this.loadMemes(this.memeNum);
  }
}
