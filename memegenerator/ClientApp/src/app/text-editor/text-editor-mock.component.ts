import { Component, Input } from "@angular/core";
import { TextEditorComponent } from "./text-editor.component";
import { fabric } from "fabric";

@Component({
  selector: "app-text-editor",
  template: "",
  providers: [{
    provide: TextEditorComponent,
    useClass: TextEditorMockComponent
  }]
})
export class TextEditorMockComponent {

  @Input('canvas') canvas: fabric.Canvas;
}
