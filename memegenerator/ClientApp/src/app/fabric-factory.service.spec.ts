import { TestBed, inject } from '@angular/core/testing';

import { FabricFactoryService } from './fabric-factory.service';

describe('FabricFactoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FabricFactoryService]
    });
  });

  it('should be created', inject([FabricFactoryService], (service: FabricFactoryService) => {
    expect(service).toBeTruthy();
  }));
});
