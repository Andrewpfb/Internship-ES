import { Component } from '@angular/core';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

import { MouseEvent } from '@agm/core';

import { MapService } from '../_services/map.service';
import { MapObject } from '../_models/mapObject';

import 'rxjs/add/operator/map';
import { MatDialog } from '@angular/material';



@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  providers: [MapService]
})
export class MapComponent implements OnInit {

  saveGeoLat: number;
  saveGeoLong: number;

  // array of markers
  markers;

  // map zoom
  zoom = 15;

  // initial center position for the map
  lat = 53.887895;
  lng = 27.538710;

  constructor(private mapService: MapService, public dialog: MatDialog) { }

  ngOnInit() {
    this.loadData('');
  }

  loadData(byTags: string) {
    this.mapService.getMapObjects(byTags).subscribe(data => {
      this.markers = data;
    });
  }
  mapDblClicked($event: MouseEvent) {
    this.markers.push({
      GeoLat: $event.coords.lat,
      GeoLong: $event.coords.lng
    });
    this.saveGeoLat = $event.coords.lat;
    this.saveGeoLong = $event.coords.lng;
  }
}
