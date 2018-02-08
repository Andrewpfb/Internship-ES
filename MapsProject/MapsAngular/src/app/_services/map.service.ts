import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { MapObject } from '../_models/mapObject';


@Injectable()
export class MapService {

  private url = 'http://localhost:50109/api/map';
  constructor(
    private http: HttpClient
  ) { }

  getMapObjects(tags: string) {
    if (tags.length === 0) {
      return this.http.get(this.url);
    } else {
      const urlParams = new HttpParams().set('tags', tags);
      return this.http.get(this.url, { params: urlParams });
    }

  }

  addMapObject(mapObject: MapObject) {
    return this.http.post(this.url, mapObject);
  }

  updateMapObject(id: number, mapObject: MapObject) {
    const urlParams = new HttpParams().set('id', id.toString());
    return this.http.put(this.url, { params: urlParams });
  }
}
