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

  public deleteTag(tagName : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.delete<GlobalError | SuccessMessage>(this.prefix + "delete" + tagName);
  }

  public changeOutputTagValue(tagName: string, value : ValueDTO) : Observable<GlobalError | SuccessMessage> {
    return this.http.put<GlobalError | SuccessMessage>(this.prefix + "output-tag-value-change/" + tagName,value);
  }
}
