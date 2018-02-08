import { Component, NgZone } from '@angular/core';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import 'rxjs/add/operator/map';

import { MapsAPILoader, MouseEvent } from '@agm/core';
import { GoogleMapsAPIWrapper } from '@agm/core/services/google-maps-api-wrapper';

import { MapService } from '../_services/map.service';
import { ChangeCoordObserverService } from '../_services/change-coord-observer.service';

import { MapObject } from '../_models/mapObject';
import { Coord } from '../_models/coord';

declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  providers: [MapService, ChangeCoordObserverService]
})
export class MapComponent extends GoogleMapsAPIWrapper implements OnInit {

  // variable for add new object.
  address: number;
  saveLat: number;
  saveLng: number;

  // array of markers
  markers;

  // map zoom
  zoom = 15;

  // initial center position for the map
  lat = 53.887895;
  lng = 27.538710;

  constructor(
    private mapService: MapService,
    private changeService: ChangeCoordObserverService,
    private __loader: MapsAPILoader,
    private __zone: NgZone
  ) {
    super(__loader, __zone);
  }

  ngOnInit() {
    this.loadData('');
  }

  loadData(byTags: string) {
    this.mapService.getMapObjects(byTags).subscribe(data => {
      this.markers = data;
    });
  }
  mapDblClicked($event: MouseEvent) {
    const lat = $event.coords.lat;
    const lng = $event.coords.lng;
    this.markers.push({
      GeoLat: lat,
      GeoLong: lng
    });
    this.changeService.coordChange(new Coord(lat, lng));
  }

  getLatLan() {
    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': this.address }, (results, status) => {
      console.log(this.markers);
      const geoLat = results[0].geometry.location.lat();
      const geoLong = results[0].geometry.location.lng();
      // Set new map center
      this.lat = geoLat;
      this.lng = geoLong;
      // Set coord for add new place
      this.saveLat = geoLat;
      this.saveLng = geoLong;
      // Set marker by coord
      this.markers.push({
        GeoLat: geoLat,
        GeoLong: geoLong
      });
    });
  }
}
