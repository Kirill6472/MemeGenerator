import { Component } from "@angular/core";
import { EditImageComponent } from "./edit-image.component";

@Component({
  selector: "app-edit-image",
  template: "",
  providers: [{
    provide: EditImageComponent,
    useClass: MockEditImageComponent
  }]
})
export class MockEditImageComponent {
  setImage() { };
  generateMeme() { };
}
