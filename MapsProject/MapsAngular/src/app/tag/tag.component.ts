import { Component, Input, ViewChild, ChangeDetectionStrategy, forwardRef } from '@angular/core';
import { MatAutocompleteSelectedEvent, MatInput } from '@angular/material';
import {
  FormControl,
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
  NG_VALIDATORS
} from '@angular/forms';

import { Tag } from '../_models/tag';

const CUSTOM_INPUT_VALIDATORS: any = {
  provide: NG_VALIDATORS,
  useExisting: forwardRef(() => TagComponent),
  multi: true
};
const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => TagComponent),
  multi: true
};

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  providers: [
    CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR
  ]
})
export class TagComponent implements ControlValueAccessor {

  @ViewChild('chipInput') chipInput: MatInput;

  @Input() source: Tag[] = [];
  @Input() _value: Tag[] = [];
  get value(): Tag[] { return this._value; }
  set value(v: Tag[]) {
    this._value = v;
    this.onChange(this._value);
  }

  onChange = (_: any): void => {
    console.log('change');
  }
  onTouched = (_: any): void => {
    console.log('touch');
  }


  writeValue(v: Tag[]): void {
    this._value = v;
  }

  sourceFiltered(): Tag[] {
    const key = 'ID';
    const reducedIds = this._value.map((o) => o[key]);
    return this.source.filter((obj: any) => reducedIds.indexOf(obj[key]) === -1);
  }

  registerOnChange(fn: (_: any) => void): void { this.onChange = fn; }
  registerOnTouched(fn: () => void): void { this.onTouched = fn; }

  validate(c: FormControl): any {
    return (this._value) ? undefined : {
      tinyError: {
        valid: false
      }
    };
  }

  add(event: MatAutocompleteSelectedEvent): void {
    const t: Tag = event.option.value;
    this._value.push(t);
    this.value = this._value;
    this.chipInput['nativeElement'].blur();
  }

  addNew(input: MatInput): void {
    const newTag: Tag = { ID: 0, TagName: input.value };
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
