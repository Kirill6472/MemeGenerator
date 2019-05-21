import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoadingImageComponent } from './loading-image/loading-image.component';
import { EditImageComponent } from './edit-image/edit-image.component';
import { ImageEditorComponent } from "./image-editor/image-editor.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoadingImageComponent,
    EditImageComponent,
    ImageEditorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
