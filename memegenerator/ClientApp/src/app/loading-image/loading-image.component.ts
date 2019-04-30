import { Component, EventEmitter, Output } from "@angular/core";
import { LoadingImageFactoryService } from "../loading-image-factory.service";

@Component({
  selector: "app-loading-image",
  templateUrl: "./loading-image.component.html",
  styleUrls: ["./loading-image.component.css"]
})
export class LoadingImageComponent {
  @Output() imageUrl = new EventEmitter<string>();
  uploadedImageUrl = "";

  constructor(private loadingImageFactory: LoadingImageFactoryService) { }

  onImageIsLoaded(event: any) {
    if (event.target.files[0]) {
      const reader = this.loadingImageFactory.createFileReader();

      reader.onload = (event: any) => {
        this.uploadedImageUrl = event.target.result;
        this.imageUrl.emit(this.uploadedImageUrl);
      }
      reader.readAsDataURL(event.target.files[0]);
    }
  }
}
