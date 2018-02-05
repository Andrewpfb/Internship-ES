import { Component } from '@angular/core';
import { MouseEvent } from '@agm/core';

import { MapService } from './map.service';

import {MapObject} from './mapObject';

import 'rxjs/add/operator/map';
import { dashCaseToCamelCase } from '@angular/compiler/src/util';
import { collectExternalReferences } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  providers: [MapService]
})
export class MapComponent {

  constructor(private mapService : MapService){}

  ngOnInit(){
    this.mapService.getObjects().subscribe(data => {
      this.markers = data;
  });
  }

  zoom: number = 15;
  
  // initial center position for the map
  lat: number = 53.887895;
  lng: number = 27.538710;

  clickedMarker(label: string, index: number) {
    console.log(`clicked the marker: ${label || index}`)
  }
  
  mapDblClicked($event: MouseEvent){
    this.markers.push({
      GeoLat: $event.coords.lat,
      GeoLong: $event.coords.lng
    });
  }

 markers
}