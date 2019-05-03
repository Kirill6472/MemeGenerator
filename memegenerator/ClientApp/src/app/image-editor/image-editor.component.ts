import { Component, ViewChild } from '@angular/core';
import { EditImageComponent } from "../edit-image/edit-image.component";

@Component({
  selector: 'app-image-editor',
  templateUrl: './image-editor.component.html',
  styleUrls: ['./image-editor.component.css']
})
export class ImageEditorComponent {
  showUploadedImage = false;

  @ViewChild(EditImageComponent)
  editImage: EditImageComponent;

  public onDisplayImage(uploadedImageUrl: string) {
    if (uploadedImageUrl !== "") {
      this.editImage.addImageToCanvas(uploadedImageUrl);
      this.hideImageLoading();
    }
  }

  public hideImageLoading() {
    this.showUploadedImage = true;
  }

  public onShowImageLoading(isUploadedImageShown: boolean) {
    this.showUploadedImage = isUploadedImageShown;
  }
}
