import {DriverType} from "./enums/DriverType";

export interface AnalogInputDTO {
  tagName : string,
  description : string,
  address : number,
  driver : DriverType,
  scanTime : number,
  isOn : boolean,
  lowLimit : number,
  highLimit : number,
  units : string
}
