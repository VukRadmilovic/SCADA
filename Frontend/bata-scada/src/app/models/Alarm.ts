import {AlarmType} from "./enums/AlarmType";

export interface Alarm {
  id: number,
  type : AlarmType,
  priority : number,
  limit : number
}
