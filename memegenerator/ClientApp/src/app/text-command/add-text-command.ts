import { EditImageComponent } from "../edit-image/edit-image.component";
import { FabricFactory } from "../fabric-factory/fabric-factory";

export class AddTextCommand implements ICommand {

  constructor(private component: EditImageComponent, private fabricFactory: FabricFactory) { }

  public execute(): void {
    this.component.canvas.add(this.fabricFactory.createText("Sample\ntext", this.component.canvas.getWidth()));
  } 
}
