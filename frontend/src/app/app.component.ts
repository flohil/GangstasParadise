import {Component, OnInit} from '@angular/core';
import {TextGeneratorService} from './text-generator.service';
import {RequestModel} from './request.model';
import {Observable} from 'rxjs/Observable';
import {ResponseModel} from './response.model';
import {MusicGeneratorService} from './music-generator.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  response$: Observable<ResponseModel>;
  audio: HTMLAudioElement;
  isPaused: boolean = true;

  constructor(private _textGeneratorService: TextGeneratorService,
              public _musicGeneratorService: MusicGeneratorService) {}

  checkTheRyme() {
    const request = new RequestModel('rapgod', 5);
    this.response$ = this._textGeneratorService.loadText(request)
  }

  beatIt() {
    this.audio = this._musicGeneratorService.beatIt();
    this.play();
  }

  pause() {
    this.audio.pause();
    this.isPaused = true;
  }

  play() {
    this.audio.play();
    this.isPaused = false;
  }

  stop() {
    this.pause();
    this.audio = undefined;
  }

}
