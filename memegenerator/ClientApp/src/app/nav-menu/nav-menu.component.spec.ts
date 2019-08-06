import { TestBed, async } from '@angular/core/testing';

import { NavMenuComponent } from './nav-menu.component';

describe('NavMenuComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        NavMenuComponent
      ],
    }).compileComponents();
  }));

  it('should create the nav-menu', () => {
    const fixture = TestBed.createComponent(NavMenuComponent);
    const navMenu = fixture.debugElement.componentInstance;

    expect(navMenu).toBeTruthy();
  });
});
