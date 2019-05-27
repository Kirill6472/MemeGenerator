import { TestBed, inject } from '@angular/core/testing';

import { TextColorService } from './text-color.service';

describe('TextColorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TextColorService]
    });
  });

  it('should be created', inject([TextColorService], (service: TextColorService) => {
    expect(service).toBeTruthy();
  }));
});
