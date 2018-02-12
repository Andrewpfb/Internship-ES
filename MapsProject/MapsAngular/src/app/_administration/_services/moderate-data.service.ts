import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

// tslint:disable-next-line:import-blacklist
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { AuthenticationService } from './authentication.service';

import { Data } from '../_models/data';


@Injectable()
export class ModerateDataService {

  headers;
  url = 'http://localhost:50109/api/moderate';
  constructor(
    private http: HttpClient,
    private authenticationService: AuthenticationService
  ) {
    this.headers = new HttpHeaders({ 'Authorization': 'Bearer ' + this.authenticationService.token });
  }

  getData(): Observable<Data[]> {
    return this.http.get<Data[]>(this.url, { headers: this.headers });
  }

  deleteData(id) {
    const urlParams = new HttpParams().set('id', id);
    return this.http.delete(this.url, { params: urlParams, headers: this.headers });
  }

  approvedObject(id) {
    const updPlace = {
      Id: id,
      Status: 'Approved'
    };
    this.headers.append({ 'Content-Type': 'application/json;charset=utf-8' });
    const urlParams = new HttpParams().set('id', id);
    return this.http.put(this.url, updPlace, { params: urlParams, headers: this.headers });
  }
}

