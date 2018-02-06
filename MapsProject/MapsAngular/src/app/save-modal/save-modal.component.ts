import { Input, Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';

import { MapService } from '../_services/map.service';
import { MapObject } from '../_models/mapObject';

@Component({
  selector: 'app-save-modal',
  templateUrl: './save-modal.component.html',
  styleUrls: ['./save-modal.component.css'],
  providers: [MapService]
})
export class SaveModalComponent implements OnInit {

  savePlaceName: string;
  saveTags: string;
  @Input() saveGeoLat: number;
  @Input() saveGeoLong: number;

  statusMessage: string;

  // property for modal window
  display = 'none';

  mapObject: MapObject;

  constructor(private mapService: MapService) {
    console.log('Const');
    console.log(this.saveGeoLat);
  }

  ngOnInit() {
    console.log('init');
    console.log(this.saveGeoLat);
  }

  openModal() {
    this.display = 'block';
  }

  onCloseHandled() {
    this.display = 'none';
  }

  savePlace() {
    this.mapObject = new MapObject(
      0,
      this.savePlaceName,
      this.saveTags,
      this.saveGeoLat,
      this.saveGeoLong
    );
    this.mapService.addMapObject(this.mapObject).subscribe(data => {
      this.statusMessage = 'Success';
    },
      // tslint:disable-next-line:no-shadowed-variable
      error => {
        console.log(error);
      });
  }

}
