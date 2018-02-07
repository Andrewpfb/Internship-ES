import { Component, NgZone, Injectable } from '@angular/core';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

import { MapsAPILoader, MouseEvent } from '@agm/core';
import { GoogleMapsAPIWrapper } from '@agm/core/services/google-maps-api-wrapper';

import { MapService } from '../_services/map.service';
import { MapObject } from '../_models/mapObject';

import 'rxjs/add/operator/map';
import { MatDialog } from '@angular/material';

declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  providers: [MapService]
})
export class MapComponent extends GoogleMapsAPIWrapper implements OnInit {

  // Алгоритм добавления объекта на карту такой. Сначала двойной клик на нужное место карты,
  // потом нажимаем на кнопку PickOne. Откроется окно, в которое передадутся координаты.
  // Так криво сделано, потому что пока не нашел как вызвать компонент с окном из кода.
  // И нельзя добавлять теги, не нашел нормального рабочего контрола.

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

  constructor(private mapService: MapService,
    private __loader: MapsAPILoader, private __zone: NgZone) {
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
    this.markers.push({
      GeoLat: $event.coords.lat,
      GeoLong: $event.coords.lng
    });
    this.saveLat = $event.coords.lat;
    this.saveLng = $event.coords.lng;
  }

  getLatLan() {
    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': this.address }, (results, status) => {
      console.log(this.markers);
      this.lat = results[0].geometry.location.lat();
      this.lng = results[0].geometry.location.lng();
      this.markers.push({
        GeoLat: results[0].geometry.location.lat(),
        GeoLong: results[0].geometry.location.lng()
      });
    });
  }
}
