import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class LoadingImageFactoryService {

  createFileReader() {
    return new FileReader();
  }
}
