import { Component, OnInit, Inject, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { TagService } from '../_services/tag.service';
import { MapService } from '../_services/map.service';
import { ChangeCoordObserverService } from '../_services/change-coord-observer.service';

import { Tag } from '../_models/tag';
import { MapObject } from '../_models/mapObject';
import { TagComponent } from '../tag/tag.component';



@Component({
  selector: 'app-save-modal',
  templateUrl: './save-modal.component.html',
  styleUrls: ['./save-modal.component.css'],
  providers: [MapService, TagComponent]
})
export class SaveModalComponent implements OnInit {

  error = '';
  success = '';

  name: string;
  lat: number;
  lng: number;
  tags: string;
  subscription: Subscription;

  constructor(
    public dialog: MatDialog,
    public snackBar: MatSnackBar,
    private mapService: MapService,
    private tagComponent: TagComponent,
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
      data: { name: this.name, lat: this.lat, lng: this.lng, error: this.error }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        const mapObj = new MapObject(0, result.name, this.tagComponent.getTags(), result.lat, result.lng);
        for (const key in mapObj) {
          if (mapObj[key] === 'undefined' || mapObj[key] === '' || mapObj.Tags === '') {
            this.error = 'Error, check fields.';
          }
        }
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

}


@Component({
  selector: 'app-save-modal-dialog',
  templateUrl: 'save-modal-dialog.html',
  providers: [TagService]
})
export class SaveModalDialogComponent implements OnInit {

  source = [];
  tags = '';
  constructor(
    public dialogRef: MatDialogRef<SaveModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tagService: TagService
  ) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    this.tagService.getTags().subscribe((data: Tag[]) => {
      data.forEach(element => {
        this.source.push(element);
      });
    });
  }
}
