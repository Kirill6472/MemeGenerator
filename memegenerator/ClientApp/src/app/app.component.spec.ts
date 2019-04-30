import { Component } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

@Component({ selector: 'app-nav-menu', template: '' })
class NavMenuStubComponent { }

@Component({ selector: 'app-image-editor', template: '' })
class ImageEditor { }

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        NavMenuStubComponent,
        ImageEditor
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
