import {AlarmType} from "./enums/AlarmType";

export interface AlarmDTO {
  type : AlarmType,
  priority : number,
  limit : number
}
