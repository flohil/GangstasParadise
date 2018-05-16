import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RequestModel} from './request.model';
import {Observable} from 'rxjs/Observable';
import {ResponseModel} from './response.model';
import 'rxjs/add/operator/map';


@Injectable()
export class TextGeneratorService {

  private static readonly OFFLINE_PATH: string = '../assets/dummyResponse.json';
  private static readonly ONLINE_PATH: string = 'http://localhost:1234/generate';

  constructor(private _httpClient: HttpClient) { }

  loadText(request: RequestModel): Observable<ResponseModel> {
    return this._httpClient.get(TextGeneratorService.ONLINE_PATH)
      .map(this.createResponseModel);
  }

  createResponseModel(text: string): ResponseModel {
    const model: ResponseModel = {
      text: text
    };
    return model;
  }

}
