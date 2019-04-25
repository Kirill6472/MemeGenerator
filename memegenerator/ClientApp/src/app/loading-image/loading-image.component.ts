import { Component, ViewChild } from '@angular/core';
import { EditImageComponent } from '../edit-image/edit-image.component';

@Component({
  selector: 'app-loading-image',
  templateUrl: './loading-image.component.html',
  styleUrls: ['./loading-image.component.css']
})
export class LoadingImageComponent {

  @ViewChild(EditImageComponent)
  private editImage: EditImageComponent;

  onImageIsLoaded(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.onload = (event: any) => {
        this.editImage.uploadedImageUrl = event.target.result;
        this.editImage.addImageToCanvas();
        this.editImage.hideImageLoading();
      }
      reader.readAsDataURL(event.target.files[0]);
    }
  }
}
