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

  public memes: MemeImage[] = [];
  private page = 1;
  private pageSize = 9;

  @Output() templateIsSelected = new EventEmitter<string>();
  
  constructor(private memeImageService: MemeImageService) { }

  ngOnInit() {
    this.loadMemes(this.page, this.pageSize); 
  }

  private loadMemes(page: number, pageSize: number) {
    this.memeImageService.getMemes(page, pageSize).subscribe((data: MemeImage[]) => this.memes = this.memes.concat(data));
  }

  public onTemplateIsSelected(event: any) {
    this.templateIsSelected.emit(event.target.src);
  }

  public onScroll() {
    this.page++;
    this.loadMemes(this.page, this.pageSize);
  }
}
