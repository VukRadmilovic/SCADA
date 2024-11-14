import {AlarmType} from "./enums/AlarmType";

export interface AlarmStampsWithPriority {
  id: number;
  limit: number;
  value: number;
  tagName : string;
  type: AlarmType;
  priority: number;
  timestamp: Date;
}
