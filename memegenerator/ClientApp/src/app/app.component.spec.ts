import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { MockNavMenuComponent } from "./nav-menu/nav-menu-mock.component";
import { MockImageEditorComponent } from "./image-editor/image-editor-mock.component";

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        MockNavMenuComponent,
        MockImageEditorComponent
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it(`should have as title 'Meme Generator'`, () => {
    expect(component.title).toEqual('Meme Generator');
  });
});
