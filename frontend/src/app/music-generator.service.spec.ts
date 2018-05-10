import { TestBed, inject } from '@angular/core/testing';

import { MusicGeneratorService } from './music-generator.service';

describe('MusicGeneratorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MusicGeneratorService]
    });
  });

  it('should be created', inject([MusicGeneratorService], (service: MusicGeneratorService) => {
    expect(service).toBeTruthy();
  }));
});
