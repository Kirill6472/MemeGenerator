import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoadingImageComponent } from './loading-image/loading-image.component';
import { EditImageComponent } from './edit-image/edit-image.component';
import { ColorPaletteComponent } from './color-palette/color-palette.component';
import { TextEditorComponent } from './text-editor/text-editor.component';
import { MemeViewerComponent } from './meme-viewer/meme-viewer.component';
import { TemplateLibraryComponent } from './template-library/template-library.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoadingImageComponent,
    EditImageComponent,
    ColorPaletteComponent,
    TextEditorComponent,
    MemeViewerComponent,
    TemplateLibraryComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    InfiniteScrollModule,
    RouterModule.forRoot([

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
