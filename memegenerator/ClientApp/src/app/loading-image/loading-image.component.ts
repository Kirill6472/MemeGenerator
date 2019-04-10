import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loading-image',
  templateUrl: './loading-image.component.html',
  styleUrls: ['./loading-image.component.css']
})
export class LoadingImageComponent implements OnInit {

  constructor() { }

  uploadedImageUrl = '';

  onImageIsLoaded(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      reader.onload = (event: any) => {
        this.uploadedImageUrl = event.target.result;
      }
    }
  }
}
