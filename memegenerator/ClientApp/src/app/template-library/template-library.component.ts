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

  @Output() templateIsSelected = new EventEmitter<string>();
  
  constructor(private memeImageService: MemeImageService) { }

  ngOnInit() {
    this.loadMemes();
  }

  private loadMemes() {
    this.memeImageService.getMemes().subscribe((data: MemeImage[]) => this.memes = data);
  }

  public onTemplateIsSelected(event: any) {
    this.templateIsSelected.emit(event.target.src);
  }
}
