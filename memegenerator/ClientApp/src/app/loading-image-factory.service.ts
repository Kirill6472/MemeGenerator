import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class LoadingImageFactoryService {

  public createFileReader() {
    return new FileReader();
  }
}
