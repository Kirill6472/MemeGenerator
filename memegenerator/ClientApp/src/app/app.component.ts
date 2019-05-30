import { Component, ViewChild, Input } from '@angular/core';
import { EditImageComponent } from './edit-image/edit-image.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Meme Generator';
  public generatedMemeUrl = "";
  public isUploadedImageShown = false;
  public isMemeDisplayed = false;
  public isMemeEdited = false;

  @ViewChild(EditImageComponent)
  editImageComponent: EditImageComponent;

  public updateImage(uploadedImageUrl: string) {
    if (uploadedImageUrl !== "") {
      this.editImageComponent.setImage(uploadedImageUrl);
      this.toggleVisualisationLoadingImage();
      this.toggleVisualisationEditImage();
    }
  }

  public setMemeUrl(url) {
    this.generatedMemeUrl = url;
    this.toggleVisualisationMemeViewer();
    this.toggleVisualisationEditImage();
  }

  public onCreateNewMeme() {
    this.toggleVisualisationMemeViewer();
    this.toggleVisualisationLoadingImage();
  }

  private toggleVisualisationLoadingImage() {
    this.isUploadedImageShown = !this.isUploadedImageShown;
  }

  private toggleVisualisationEditImage() {
    this.isMemeEdited = !this.isMemeEdited;
  }

  private toggleVisualisationMemeViewer() {
    this.isMemeDisplayed = !this.isMemeDisplayed;
  }
}
