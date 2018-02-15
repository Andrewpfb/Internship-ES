import { Component, NgZone, ChangeDetectionStrategy, Inject } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { MatDialog, MatSnackBar, VERSION, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { MapsAPILoader, MouseEvent } from '@agm/core';
import { GoogleMapsAPIWrapper } from '@agm/core/services/google-maps-api-wrapper';

import { MapService } from '../_services/map.service';
import { ChangeTagObserverService } from '../_services/change-tag-observer.service';
import { TagService } from '../_services/tag.service';

import { MapObject } from '../_models/mapObject';
import { Coord } from '../_models/coord';
import { Tag } from '../_models/tag';

declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  providers: [MapService]
})
export class MapComponent extends GoogleMapsAPIWrapper implements OnInit {

  // variable for add new object.
  address: number;
  saveLat: number;
  saveLng: number;
  tags = [];
  subscription: Subscription;

  lastMarker;

  // array of markers
  markers;

  // map zoom
  zoom = 15;

  // initial center position for the map
  lat = 53.887895;
  lng = 27.538710;

  constructor(
    private mapService: MapService,
    private changeService: ChangeTagObserverService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private __loader: MapsAPILoader,
    private __zone: NgZone
  ) {
    super(__loader, __zone);
    this.subscription = changeService.changeTag$.subscribe(
      tag => {
      this.tags = tag;
    });
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
    if (this.lastMarker) {
      this.markers.pop();
    }
    this.lastMarker = { GeoLat: lat, GeoLong: lng };
    this.markers.push({
      GeoLat: lat,
      GeoLong: lng
    });
    this.openSaveModal(lat, lng);
  }

  openSaveModal(lat, lng): void {

    let name: string;
    let errorMessage: string;
    let successMessage: string;

    const dialogRef = this.dialog.open(SaveModalDialogComponent, {
      width: '250px',
      data: { name: name, lat: lat, lng: lng, error: errorMessage } // tags: [],
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('confirm');
      console.log(this.tags);
      if (result !== undefined) {
        const mapObj = new MapObject(
          0,
          result.name,
          this.getTagsFromArrayToString(this.tags),
          result.lat,
          result.lng);
        errorMessage = this.checkObjForError(mapObj);
        if (errorMessage) {
          name = result.name;
          lat = result.lat;
          lng = result.lng;
          this.openSaveModal(lat, lng);
        } else {
          this.mapService.addMapObject(mapObj).subscribe(
            data => {
              successMessage = 'Place was added for moderation';
              this.snackBar.open(successMessage, '', { duration: 2000, });
            },
            error => {
              error = 'Error, check fields.';
              this.openSaveModal(lat, lng);
            });
        }
        name = 'placeName';
        errorMessage = '';
      }
    });
  }

  getTagsFromArrayToString(array: any) {
    let tagString = array.tags.map(data => {
      return data.TagName;
    });
    tagString = tagString.join(';');
    return tagString;
  }

  checkObjForError(obj: any) {
    for (const key in obj) {
      if (obj[key] === 'undefined' || obj[key] === '') {
        return 'Error, check fields.';
      }
    }
  }

  getLatLan() {
    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': this.address }, (results, status) => {
      console.log(results);
      const geoLat = results[0].geometry.location.lat();
      const geoLong = results[0].geometry.location.lng();
      // Set new map center
      this.lat = geoLat;
      this.lng = geoLong;
      // Set coord for add new place
      this.saveLat = geoLat;
      this.saveLng = geoLong;
      if (this.lastMarker) {
        this.markers.pop();
      }
      this.lastMarker = { GeoLat: geoLat, GeoLong: geoLong };
      // Set marker by coord
      this.markers.push({
        GeoLat: geoLat,
        GeoLong: geoLong
      });
    });
  }
}

@Component({
  selector: 'app-save-modal-dialog',
  templateUrl: 'save-modal-dialog.html',
  providers: [TagService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SaveModalDialogComponent implements OnInit {

  source = [];
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<SaveModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tagService: TagService,
    private changeService: ChangeTagObserverService,
    private _fb: FormBuilder
  ) {
    this.initForm();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    this.tagService.getTags().subscribe((data: any[]) => {
      data.forEach(element => {
        this.source.push({ ID: element.Id, TagName: element.TagName });
      });
    });
  }

  initForm() {
    this.form = this._fb.group({
      tags: [[]]
    });

    this.form.valueChanges
      .debounceTime(300)
      .distinctUntilChanged().subscribe((data: any) => {
        this.changeService.tagChange(data);
      });
  }
}
