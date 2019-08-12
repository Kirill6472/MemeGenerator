import { TestBed, async, ComponentFixture } from '@angular/core/testing';

import { NavMenuComponent } from './nav-menu.component';

describe('NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        NavMenuComponent
      ],
    }).compileComponents();

    fixture = TestBed.overrideComponent(NavMenuComponent, {
      set: {
        selector: 'app-nav-menu',
        template: '<div></div>'
      }
    }).createComponent(NavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create the nav-menu', () => {
    expect(component).toBeTruthy();
  });
});
