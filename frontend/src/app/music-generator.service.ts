import { Injectable } from '@angular/core';

@Injectable()
export class MusicGeneratorService {

  private static readonly OFFLINE_PATH: string = '../assets/SampleAudio_0.4mb.mp3';
  private static readonly ONLINE_PATH: string = 'http://localhost:1234/mix';

  constructor() { }

  beatIt() {
    const audio = new Audio();
    audio.src = MusicGeneratorService.ONLINE_PATH + "?id=" + Date.now();
    audio.load();
    return audio;
  }
}
