import { Component, ViewChild } from '@angular/core';
import { EditImageComponent } from './edit-image/edit-image.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Meme Generator';
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
