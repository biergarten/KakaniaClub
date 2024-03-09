import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class UserActivityService {
    private userActivity = new Subject<void>();
    private timeoutMilliseconds = 10000; // 5 minutes of inactivity

    constructor() {
        this.watchForInactivity();
    }

    private watchForInactivity() {
        // Listen to the userActivity subject and debounce the events
        // Only trigger after 5 minutes (300000 milliseconds) of inactivity
        this.userActivity.pipe(
            debounceTime(this.timeoutMilliseconds) // 5 minutes in milliseconds
        ).subscribe(() => {
            this.showInactivePopup();
        });

        this.setupActivityListeners();
    }

    private setupActivityListeners() {
        // Listen for any user activities
        ['click', 'mousemove', 'keypress'].forEach(event => {
            window.addEventListener(event, () => {
                console.log('escuchando');
                this.userActivity.next();
            });
        });
    }

    private showInactivePopup() {
        // Logic to show the popup
        console.log('inactive');
        //alert('You have been inactive for 5 minutes.');
    }
}
