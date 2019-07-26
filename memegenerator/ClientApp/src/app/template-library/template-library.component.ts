import { Component, OnInit } from '@angular/core';
import { MemeImageService } from '../services/meme-image.service';
import { MemeImage } from '../models/meme-image';

@Component({
  selector: 'app-template-library',
  templateUrl: './template-library.component.html',
  styleUrls: ['./template-library.component.css'],
  providers: [MemeImageService]
})
export class TemplateLibraryComponent implements OnInit {

  memeImage = new MemeImage();
  memes: MemeImage[];
  
  constructor(private memeImageService: MemeImageService) { }

  ngOnInit() {
    this.loadMemes();
  }

  loadMemes() {
    this.memeImageService.getMemes().subscribe((data: MemeImage[]) => this.memes = data);
  }
}
