import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class FileReaderFactoryService {

  public createFileReader() {
    return new FileReader();
  }
}
