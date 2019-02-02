import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {RequestModel} from './request.model';
import {Observable} from 'rxjs/Observable';
import {ResponseModel} from './response.model';
import 'rxjs/add/operator/map';


@Injectable()
export class TextGeneratorService {

  private static readonly API_PROXY_PATH: string = 'http://localhost:1234/generate';

  constructor(private _httpClient: HttpClient) { }

  loadText(request: RequestModel): Observable<ResponseModel> {
    // need to use post request because backend can only load body payload for post requests

    return this._httpClient.post(
      TextGeneratorService.API_PROXY_PATH,
      JSON.stringify(request),
      {
        responseType: 'text'
      }
    ).map(res => this.createResponseModel(res));
  }

  createResponseModel(text: string): ResponseModel {
    const model: ResponseModel = {
      text: text.trim()
    };
    return model;
  }
}
