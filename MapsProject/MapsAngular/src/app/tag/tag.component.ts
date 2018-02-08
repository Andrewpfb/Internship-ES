import { Component, Input, ViewChild } from '@angular/core';
import { MatAutocompleteSelectedEvent, MatInput } from '@angular/material';

import { Tag } from '../_models/tag';
import { element } from 'protractor';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html'
})
export class TagComponent {

  static tags = '';
  @ViewChild('chipInput') chipInput: MatInput;

  test;

  @Input() source: Tag[] = [];
  @Input() _value: Tag[] = [];
  get value(): Tag[] { return this._value; }
  set value(v: Tag[]) {
    this._value = v;
    this._value.forEach(tag => {
      TagComponent.tags += tag.TagName + ';';
    });
  }

  getTags() {
    const tagsArray = TagComponent.tags.split(';');
    TagComponent.tags = '';
    const uniqueArray = tagsArray.filter(function (item, pos) {
      return tagsArray.indexOf(item) === pos;
    });
    let returnTagsString = '';
    uniqueArray.forEach(tagName => {
      if (tagName === '') { } else {
        returnTagsString += tagName + ';';
      }
    });
    return returnTagsString;
  }

  sourceFiltered(): Tag[] {
    const key = 'Id';
    const reducedIds = this._value.map((o) => o[key]);
    return this.source.filter((obj: any) => reducedIds.indexOf(obj[key]) === -1);
  }

  add(event: MatAutocompleteSelectedEvent): void {
    const t: Tag = event.option.value;
    this._value.push(t);
    this.value = this._value;
    this.chipInput['nativeElement'].blur();
  }

  addNew(input: MatInput): void {
    // create a tmp id for interaction until the api has assigned a new one
    const newId: number = Math.floor(Math.random() * (100000 - 10000 + 1)) + 10000;
    const newTag: Tag = { ID: newId, TagName: input.value };
    this._value.push(newTag);
    this.value = this._value;
    this.chipInput['nativeElement'].value = '';
    this.source.push(newTag);
  }

  remove(tag: Tag): void {
    const index = this._value.indexOf(tag);
    if (index >= 0) {
      this._value.splice(index, 1);
    }
    this.value = this._value;
    this.chipInput['nativeElement'].blur();
  }

  displayFn(value: any): string {
    return value && typeof value === 'object' ? value.text : value;
  }

}
