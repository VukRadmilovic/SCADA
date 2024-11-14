import {AlarmType} from "./enums/AlarmType";

export interface AlarmWithTagNameDTO {
  id: number,
  type : AlarmType,
  priority : number,
  limit : number,
  tagName: string
}
