import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Coord } from '../_models/coord';

@Injectable()
export class ChangeCoordObserverService {

  // Observable coord sources
  private coordSource = new Subject<Coord>();

  // Observable coord streams
  changeCoord$ = this.coordSource.asObservable();

  // Service message command
  coordChange(coord: Coord) {
    this.coordSource.next(coord);
  }
}
