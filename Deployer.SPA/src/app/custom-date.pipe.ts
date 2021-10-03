import { Pipe, PipeTransform } from '@angular/core';
import { formatDate } from '@angular/common';

@Pipe({ name: 'customDate' })
export class CustomDatePipe implements PipeTransform {
    transform(value: string): string {
        if (value == null)
            return "-"
        return formatDate(value, 'dd.MM.yyyy, HH:mm', "pl");
    }
}