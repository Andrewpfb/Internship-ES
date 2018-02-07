import { Input, Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MapsAPILoader } from '@agm/core';

import { TagService } from '../_services/tag.service';
import { FormControl } from '@angular/forms';
import { Tag } from '../_models/tag';
import { MapService } from '../_services/map.service';
import { MapObject } from '../_models/mapObject';

let tagsString = '';

@Component({
  selector: 'app-save-modal',
  templateUrl: './save-modal.component.html',
  styleUrls: ['./save-modal.component.css'],
  providers: [MapService]
})
export class SaveModalComponent implements OnInit {

  name = 'placeName';
  @Input() lat = 52;
  @Input() lng = 52;

  constructor(public dialog: MatDialog, private mapService: MapService) { }

  ngOnInit() { }

  openDialog(): void {
    const dialogRef = this.dialog.open(SaveModalDialogComponent, {
      width: '250px',
      data: { name: this.name, lat: this.lat, lng: this.lng }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
      if (result !== undefined) {
        const mapObj = new MapObject(0, result.name, tagsString, result.lat, result.lng);
        this.mapService.addMapObject(mapObj).subscribe(data => {
          console.log('Success');
        });
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

  tags = new FormControl();
  selected = [];
  tagString = '';
  tagList: Array<string>;

  constructor(
    public dialogRef: MatDialogRef<SaveModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tagService: TagService) { this.tagList = new Array<string>(); }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    this.tagService.getTags().subscribe((data: Tag[]) => {
      data.forEach(element => {
        this.tagList.push(element.TagName);
      });
    });
  }

  getTagsFromSelect() {
    console.log(this.selected);
    this.tagString = '';
    this.selected.forEach(element => {
      console.log(element);
      tagsString += element + ';';
    });
  }
}
