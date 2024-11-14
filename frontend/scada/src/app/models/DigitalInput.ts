import {DriverType} from "./enums/DriverType";

export interface DigitalInput {
  tagName : string,
  description : string,
  address : number,
  driver : DriverType,
  scanTime : number,
  isOn : boolean
}
