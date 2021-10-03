import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';
import * as momentDurationFormatSetup from 'moment-duration-format'

@Pipe({ name: 'timeSpan' })
export class TimeSpanPipe implements PipeTransform {
    transform(value: string): string {
        if(value == null){
            return ""
        }
        momentDurationFormatSetup(moment);
        var duration = moment.duration(value) as any
        return duration.format("h:mm:ss")
    }
}
