import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {RequestModel} from './request.model';
import {Observable} from 'rxjs/Observable';
import {ResponseModel} from './response.model';
import 'rxjs/add/operator/map';


@Injectable()
export class TextGeneratorService {

  private static readonly OFFLINE_PATH: string = '../assets/dummyResponse.json';
  private static readonly ONLINE_PATH: string = 'http://localhost:1234/generate';
  private static readonly API_PROXY_PATH: string = '/api/generate'

  constructor(private _httpClient: HttpClient) { }

  loadText(request: RequestModel): Observable<ResponseModel> {
    // need to use post request because backend can only load body payload for post requests
    return this._httpClient.post<any>(
      TextGeneratorService.API_PROXY_PATH,
      JSON.stringify(request)
    ).map(text => this.createResponseModel(text))
  }

  createResponseModel(text: string): ResponseModel {
    const model: ResponseModel = {
      text: text
    };
    return model;
  }

}
