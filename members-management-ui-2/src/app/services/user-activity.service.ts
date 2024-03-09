// inactivity.service.ts
import { Injectable } from '@angular/core';
import { fromEvent, merge, Observable, timer } from 'rxjs';
import { switchMap, startWith } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class UserActivityService {
    inactivityThreshold$ = timer(20 * 1000);
    beforeUnloadObservable$ = fromEvent(window, 'beforeunload'); //.pipe(mapTo('appClosed'));


    activityEvents$ = merge(
        fromEvent(document, 'mousemove'),
        fromEvent(document, 'keypress'),
        fromEvent(document, 'click')
    );

    inactivityObservable$: Observable<number> = this.activityEvents$.pipe(
        startWith(0),
        // Restart the timer on every user activity
        switchMap(() => this.inactivityThreshold$)
    );

    constructor() { }
}
