import { Pipe, PipeTransform } from '@angular/core';
import { formatDate } from '@angular/common';

@Pipe({ name: 'customHours' })
export class CustomHoursPipe implements PipeTransform {
    transform(value: number): string {
        let hoursInt = value / 60;
        let minutesInt = value % 60;

        let hours: string = hoursInt.toString();
        let minutes: string = minutesInt.toString();

        if(hoursInt < 10){
            hours = "0" + hours;
        }

        if(minutesInt < 10){
            minutes = "0" + minutes;
        }

        return hours + ":" + minutes;
    }
}