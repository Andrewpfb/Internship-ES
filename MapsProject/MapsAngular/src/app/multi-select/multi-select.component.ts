import { Component, OnInit, Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';
import { TagService } from '../_services/tag.service';
import { Tag } from '../_models/tag';
import { MapComponent } from '../map/map.component';



@Component({
  selector: 'app-multi-select',
  templateUrl: './multi-select.component.html',
  styleUrls: ['./multi-select.component.css'],
  providers: [TagService]
})
@Injectable()
export class MultiSelectComponent implements OnInit {
  tags = new FormControl();
  selected = [];
  tagString = '';
  tagList: Array<string>;

  constructor(private tagService: TagService, private mapComponent: MapComponent) {
    this.tagList = new Array<string>();
  }

  ngOnInit() {
    this.tagService.getTags().subscribe((data: Tag[]) => {
      data.forEach(element => {
        this.tagList.push(element.TagName);
      });
    });
  }

  getTagsFromSelect() {
    this.tagString = '';
    this.selected.forEach(element => {
      this.tagString += element + ';';
    });
    this.mapComponent.loadData(this.tagString);
  }
}
