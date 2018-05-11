import { TestBed, inject } from '@angular/core/testing';

import { TextGeneratorService } from './text-generator.service';

describe('TextGeneratorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TextGeneratorService]
    });
  });

  it('should be created', inject([TextGeneratorService], (service: TextGeneratorService) => {
    expect(service).toBeTruthy();
  }));
});
