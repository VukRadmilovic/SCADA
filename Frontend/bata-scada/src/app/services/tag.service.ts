import { Injectable } from '@angular/core';
import {environment} from "../environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {OutputTagDTO} from "../models/OutputTagDTO";
import {InputTagDTO} from "../models/InputTagDTO";
import {GlobalError} from "../models/GlobalError";
import {SuccessMessage} from "../models/SuccessMessage";
import {DigitalOutput} from "../models/DigitalOutput";
import {AnalogOutput} from "../models/AnalogOutput";
import {ValueDTO} from "../models/ValueDTO";
import {DigitalInput} from "../models/DigitalInput";
import {AnalogInputDTO} from "../models/AnalogInputDTO";
import {Alarm} from "../models/Alarm";
import {AlarmDTO} from "../models/AlarmDTO";
import {TagValueWithoutNameDTO} from "../models/TagValueWithoutNameDTO";
import {TagValueWithTimestamp} from "../models/TagValueWithTimestamp";
import {FormControl, ɵValue} from "@angular/forms";
import {AlarmStampsWithPriority} from "../models/AlarmStampsWithPriority";

@Injectable({
  providedIn: 'root'
})
export class TagService {

  prefix = environment.apiURL + "tags/"
  constructor(private http: HttpClient) { }

  public getAllOutputTags() : Observable<OutputTagDTO[]> {
    return this.http.get<OutputTagDTO[]>(this.prefix + "output-tags")
  }

  public getAllInputTags() : Observable<InputTagDTO[]> {
    return this.http.get<InputTagDTO[]>(this.prefix + "input-tags")
  }

  public addDigitalOutputTag(tag : DigitalOutput) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "add-digital-output",tag);
  }

  public addAnalogOutputTag(tag : AnalogOutput) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "add-analog-output",tag);
  }

  public addDigitalInputTag(tag : DigitalInput) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "add-digital-input",tag);
  }

  public addAnalogInputTag(tag : AnalogInputDTO) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "add-analog-input",tag);
  }

  public deleteTag(tagName : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.delete<GlobalError | SuccessMessage>(this.prefix + "delete/" + tagName);
  }

  public changeOutputTagValue(tagName: string, value : ValueDTO) : Observable<GlobalError | SuccessMessage> {
    return this.http.put<GlobalError | SuccessMessage>(this.prefix + "output-tag-value-change/" + tagName,value);
  }

  public turnOnScan( tagName : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.put<GlobalError | SuccessMessage>(this.prefix + "turn-on-scan/" + tagName,null);
  }

  public turnOffScan( tagName : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.put<GlobalError | SuccessMessage>(this.prefix + "turn-off-scan/" + tagName,null);
  }

  public scan(tagName : string): Observable<GlobalError | number> {
    return this.http.get<GlobalError | number>(this.prefix + "scan-input-tag/" + tagName);
  }

  public addAlarm(tagName : string, alarm: AlarmDTO) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "add-alarm/" + tagName, alarm);
  }

  public tagLastValue(tagName : string): Observable<GlobalError | number> {
    return this.http.get<GlobalError | number>(this.prefix + "tag-last-value/" + tagName);
  }
  public tagAllValues(tagName : string): Observable<TagValueWithoutNameDTO[]>{
    return this.http.get<TagValueWithoutNameDTO[]>(this.prefix + "tag-all-values/" + tagName);
  }

  public digitalAllValues(): Observable<TagValueWithTimestamp[]>{
    return this.http.get<TagValueWithTimestamp[]>(this.prefix + "digital-all-last/");
  }

  public analogAllValues(): Observable<TagValueWithTimestamp[]>{
    return this.http.get<TagValueWithTimestamp[]>(this.prefix + "analog-all-last/");
  }

  public allTime(from: Date | null, to: Date | null): Observable<TagValueWithTimestamp[]>{
    return this.http.get<TagValueWithTimestamp[]>(this.prefix + "all-time/" + from?.getTime().toString() + "/" + to?.getTime().toString());
  }

  public someTime(from: Date | null, to: Date | null): Observable<AlarmStampsWithPriority[]>{
    return this.http.get<AlarmStampsWithPriority[]>(environment.apiURL + "alarms/some-time/" + from?.getTime().toString() + "/" + to?.getTime().toString());
  }

  public alarmWPriority(priority: string): Observable<AlarmStampsWithPriority[]>{
    return this.http.get<AlarmStampsWithPriority[]>(environment.apiURL + "alarms/wpriority/" + priority);
  }
}
