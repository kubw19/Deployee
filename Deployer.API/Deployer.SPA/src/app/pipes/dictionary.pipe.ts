import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'dictionary' })
export class DictionaryPipe implements PipeTransform {
    transform(value: string, type: DictionaryType): string {
        switch (+type) {
            case DictionaryType.TimetableRequestStatus:
                return this.timetableRequestStatus(value);
            default:
                return "";

        }
    }

    timetableRequestStatus(value: any) {
        switch (value) {
            case "Submitted":
                return "Złożony"
            case "Rejected":
                return "Odrzucony"
            case "Returned":
                return "Zwrócony";
            case "Accepted":
                return "Zaakceptowany";
        }
    }

}

export enum DictionaryType {
    TimetableRequestStatus = 0
}
