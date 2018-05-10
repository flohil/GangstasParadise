import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RequestModel} from './request.model';
import {Observable} from 'rxjs/Observable';
import {ResponseModel} from './response.model';
import 'rxjs/add/operator/map';


@Injectable()
export class TextGeneratorService {

  constructor(private _httpClient: HttpClient) { }

  loadText(request: RequestModel): Observable<ResponseModel> {
    return this._httpClient.get('../assets/dummyResponse.json')
      .map(next => next as ResponseModel);
  }

}
