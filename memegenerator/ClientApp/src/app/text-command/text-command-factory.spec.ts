import { TestBed, inject } from '@angular/core/testing';

import { TextCommandFactory } from './text-command-factory';

describe('TextCommandFactoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TextCommandFactory]
    });
  });

  it('should be created', inject([TextCommandFactory], (service: TextCommandFactory) => {
    expect(service).toBeTruthy();
  }));
});
