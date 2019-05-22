import { Injectable } from '@angular/core';
import { AddTextCommand } from "./add-text-command";
import { EditImageComponent } from '../edit-image/edit-image.component';
import { FabricFactory } from '../fabric-factory/fabric-factory';

@Injectable({
  providedIn: 'root'
})
export class TextCommandFactory {

  constructor(private fabricFactory: FabricFactory) {}

  public createTextCommand(component: EditImageComponent) {
    return new AddTextCommand(component, this.fabricFactory);
  }
}
