import {DriverType} from "./enums/DriverType";
import {Alarm} from "./Alarm";
import {TagType} from "./enums/TagType";

export interface InputTagDTO {
  tagName : string,
  description : string,
  address : number,
  driver : DriverType,
  scanTime : number,
  isOn : boolean,
  lowLimit : number,
  highLimit : number,
  units : string,
  type : TagType,
  value : number,
  alarms : Alarm[]
}
