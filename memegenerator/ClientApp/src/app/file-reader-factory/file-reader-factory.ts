import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class FileReaderFactory {

  public createFileReader() {
    return new FileReader();
  }
}
