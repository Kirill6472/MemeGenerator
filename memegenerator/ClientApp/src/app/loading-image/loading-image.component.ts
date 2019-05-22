import { Component, EventEmitter, Output } from "@angular/core";
import { FileReaderFactory } from "../file-reader-factory/file-reader-factory";

@Component({
  selector: "app-loading-image",
  templateUrl: "./loading-image.component.html",
  styleUrls: ["./loading-image.component.css"]
})
export class LoadingImageComponent {
  @Output() imageIsUploaded = new EventEmitter<string>();

  constructor(private loadingImageFactory: FileReaderFactory) { }

  public onImageIsLoaded(event: any) {
    if (event.target.files.length && event.target.files[0]) {
      const reader = this.loadingImageFactory.createFileReader();

      reader.onload = (event: any) => {
        this.imageIsUploaded.emit(event.target.result);
      }
      reader.readAsDataURL(event.target.files[0]);
    }
  }
}
