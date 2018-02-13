import { Component, OnInit, Inject, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { VERSION } from '@angular/material';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { TagService } from '../_services/tag.service';
import { MapService } from '../_services/map.service';
import { ChangeCoordObserverService } from '../_services/change-coord-observer.service';

import { Tag } from '../_models/tag';
import { MapObject } from '../_models/mapObject';

let tags = [];

@Component({
  selector: 'app-save-modal',
  templateUrl: './save-modal.component.html',
  styleUrls: ['./save-modal.component.css'],
  providers: [MapService]
})
export class SaveModalComponent implements OnInit {

  error = '';
  success = '';

  name: string;
  lat: number;
  lng: number;
  subscription: Subscription;

  constructor(
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    private mapService: MapService,
    private changeCoordObserverService: ChangeCoordObserverService
  ) {
    this.name = 'placeName';
    this.subscription = changeCoordObserverService.changeCoord$.subscribe(
      coord => {
        this.lat = coord.Lat;
        this.lng = coord.Lng;
        this.openDialog();
      });
  }

  ngOnInit() { }

  openDialog(): void {
    const dialogRef = this.dialog.open(SaveModalDialogComponent, {
      width: '250px',
      data: { name: this.name, tags: tags, lat: this.lat, lng: this.lng, error: this.error }
    });

    dialogRef.afterClosed().subscribe(result => {
      tags = [];
      if (result !== undefined) {
        const mapObj = new MapObject(
          0,
          result.name,
          this.getTagsFromArrayToString(result.tags),
          result.lat,
          result.lng);
        this.checkObjForError(mapObj);
        if (this.error) {
          this.name = result.name;
          this.lat = result.lat;
          this.lng = result.lng;
          this.openDialog();
        } else {
          this.mapService.addMapObject(mapObj).subscribe(
            data => {
              this.success = 'Place was added for moderation';
              this.snackBar.open(this.success, '', { duration: 2000, });
            },
            error => {
              this.error = 'Error, check fields.';
              this.openDialog();
            });
        }
        this.name = 'placeName';
        this.error = '';
      }
    });
  }

  getTagsFromArrayToString(array: any) {
    let tagString = array.map(data => {
      return data.TagName;
    });
    tagString = tagString.join(';');
    return tagString;
  }

  checkObjForError(obj: any) {
    for (const key in obj) {
      if (obj[key] === 'undefined' || obj[key] === '') {
        this.error = 'Error, check fields.';
      }
    }
  }

}


@Component({
  selector: 'app-save-modal-dialog',
  templateUrl: 'save-modal-dialog.html',
  providers: [TagService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SaveModalDialogComponent implements OnInit {

  version = VERSION;
  source = [];
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<SaveModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tagService: TagService,
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
      tags: [tags]
    });

    this.form.valueChanges
      .debounceTime(300)
      .distinctUntilChanged().subscribe((data: Tag[]) => {
        tags = data;
      });
  }
}
