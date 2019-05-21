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
  editImageComponent: EditImageComponent;

  public updateImage(uploadedImageUrl: string) {
    if (uploadedImageUrl !== "") {
      this.editImageComponent.setImage(uploadedImageUrl);
      this.showUploadedImage = true;
    }
  }

  public isDisplayImageInput(isUploadedImageShown: boolean) {
    this.showUploadedImage = isUploadedImageShown;
  }
}
