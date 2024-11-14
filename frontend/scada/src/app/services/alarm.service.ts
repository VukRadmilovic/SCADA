import { Injectable } from '@angular/core';
import {environment} from "../environment";
import {HttpClient} from "@angular/common/http";
import {SuccessMessage} from "../models/SuccessMessage";
import {GlobalError} from "../models/GlobalError";
import {Observable} from "rxjs";
import {Alarm} from "../models/Alarm";
import {AlarmWithTagNameDTO} from "../models/AlarmWithTagNameDTO";

@Injectable({
  providedIn: 'root'
})
export class AlarmService {
  prefix = environment.apiURL + "alarms/"
  constructor(private http: HttpClient) { }

  public deleteAlarm(id : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.delete<GlobalError | SuccessMessage>(this.prefix + "delete/" + id);
  }

  public getActive() : Observable<AlarmWithTagNameDTO[]> {
    return this.http.get<AlarmWithTagNameDTO[]>(this.prefix + "active");
  }

  public snooze(id : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.put<GlobalError | SuccessMessage>(this.prefix + "snooze/" + id,null);
  }
}
