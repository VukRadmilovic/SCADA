import { Injectable } from '@angular/core';
import {environment} from "../environment";
import {HttpClient} from "@angular/common/http";
import {SuccessMessage} from "../models/SuccessMessage";
import {GlobalError} from "../models/GlobalError";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AlarmService {
  prefix = environment.apiURL + "alarms/"
  constructor(private http: HttpClient) { }

  public deleteAlarm(id : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.delete<GlobalError | SuccessMessage>(this.prefix + "delete/" + id);
  }
}
