import { Injectable } from '@angular/core';

@Injectable()
export class MusicGeneratorService {

  constructor() { }

  beatIt() {
    const audio = new Audio();
    audio.src = "../assets/SampleAudio_0.4mb.mp3";
    audio.load();
    return audio;
  }

}
