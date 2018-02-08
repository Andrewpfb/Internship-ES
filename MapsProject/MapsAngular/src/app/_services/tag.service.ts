import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class TagService {

  private url = 'http://localhost:50109/api/tags';
  constructor(
    private http: HttpClient
  ) { }

  getTags() {
    return this.http.get(this.url);
  }
}
