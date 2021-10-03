export class Helpers {
    public static UTCtoLocalDateString(date: Date | string): string {
        if (typeof date == "string") {
            date = new Date(date)
        }

        var offset = date.getTimezoneOffset() / 60;
        var hours = date.getHours();

        date.setHours(hours - offset);

        return date.toISOString().slice(0, 16)
    }

    public static LocalDateToUTCString(date: Date | string): string {
        if (typeof date == "string") {
            date = new Date(date)
        }
        return date.toISOString()
    }

    public static secondsToTime(_seconds: number): string {
        let hour: any = Math.floor(_seconds / 3600)
        let minute: any = Math.floor(_seconds / 60 % 60)
        let seconds: any = Math.floor(_seconds % 60)
        if (minute < 10) {
            minute = "0" + minute
        }
        if (seconds < 10) {
            seconds = "0" + seconds
        }
        return hour + ":" + minute + ":" + seconds
    }

    public static millisecondsToTime(milliseconds): string {
        return this.secondsToTime(milliseconds / 1000)
    }
}