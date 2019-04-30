import { Component, ViewChild } from '@angular/core';
import { EditImageComponent } from "../edit-image/edit-image.component";

@Component({
  selector: 'app-loading-and-edit-image',
  templateUrl: './loading-and-edit-image.component.html',
  styleUrls: ['./loading-and-edit-image.component.css']
})
export class LoadingAndEditImageComponent {
  showUploadedImage = false;

  @ViewChild(EditImageComponent)
  editImage: EditImageComponent;

  onDisplayImage(uploadedImageUrl: string) {
    if (uploadedImageUrl !== "") {
      this.editImage.addImageToCanvas(uploadedImageUrl);
      this.hideImageLoading();
    }
  }

  hideImageLoading() {
    this.showUploadedImage = true;
  }

  onShowImageLoading(showUploadImage: boolean) {
    this.showUploadedImage = showUploadImage;
  }
}
