import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';



@Pipe({
  name: 'customDate',
  pure: true
})
export class CustomDatePipe extends DatePipe  implements PipeTransform {

  transform(value: any, pattern: string = 'dd MMMM yyyy in H:mm'): string | null {
    const result = super.transform(value, pattern);
    return result;
  }
}
