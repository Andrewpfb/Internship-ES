import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Coord } from '../_models/coord';
import { Tag } from '../_models/tag';

@Injectable()
export class ChangeTagObserverService {

  // Observable coord sources
 // private coordSource = new Subject<Coord>();
  private tagSource = new Subject<any>();


  // Observable coord streams
  // changeCoord$ = this.coordSource.asObservable();
  changeTag$ = this.tagSource.asObservable();

  // Service message command
  // coordChange(coord: Coord) {
  //   this.coordSource.next(coord);
  // }

  tagChange(tag: any) {
    this.tagSource.next(tag);
  }
}
