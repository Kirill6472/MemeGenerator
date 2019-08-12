import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorPaletteComponent } from './color-palette.component';

describe('ColorPaletteComponent', () => {
  let component: ColorPaletteComponent;
  let fixture: ComponentFixture<ColorPaletteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ColorPaletteComponent ]
    }).compileComponents();

    fixture = TestBed.overrideComponent(ColorPaletteComponent, {
      set: {
        selector: 'app-color-palette',
        template: '<div></div>'
      }
    }).createComponent(ColorPaletteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set selected color', () => {
    const fakeEvent = {
      target: {
        value: "#000000"
      }
    };
    spyOn(component.selectedColorChange, "emit");

    component.setColor(fakeEvent);

    expect(component.selectedColorChange.emit).toHaveBeenCalledWith(fakeEvent.target.value);
  });
});
