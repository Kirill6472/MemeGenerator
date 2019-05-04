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

  public displayImage(uploadedImageUrl: string) {
    if (uploadedImageUrl !== "") {
      this.editImage.addImageToCanvas(uploadedImageUrl);
      this.showUploadedImage = true;
    }
  }

  public showImageLoading(isUploadedImageShown: boolean) {
    this.showUploadedImage = isUploadedImageShown;
  }
}
