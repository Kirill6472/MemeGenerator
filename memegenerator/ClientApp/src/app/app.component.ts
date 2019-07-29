import { Component, ViewChild } from '@angular/core';
import { EditImageComponent } from './edit-image/edit-image.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Meme Generator';
  public generatedMemeUrl = "";
  public isImageSelectorVisible = true;
  public isMemeViewerVisible = false;
  public isImageEditorVisible = false;

  @ViewChild(EditImageComponent)
  editImageComponent: EditImageComponent;

  public updateImage(uploadedImageUrl: string) {
    if (uploadedImageUrl !== "") {
      this.editImageComponent.setImage(uploadedImageUrl);
      this.isImageSelectorVisible = false;
      this.isImageEditorVisible = true;
    }
  }

  public setMemeUrl(url) {
    this.generatedMemeUrl = url;
  }

  public createNewMeme() {
    this.isMemeViewerVisible = false;
    this.isImageSelectorVisible = true;
  }

  public generateMeme() {
    this.editImageComponent.generateMeme();

    this.isMemeViewerVisible = true;
    this.isImageEditorVisible = false;
  }
}
