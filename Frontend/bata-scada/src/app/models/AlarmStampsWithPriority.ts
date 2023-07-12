import {AlarmType} from "./enums/AlarmType";

export interface AlarmStampsWithPriority {
  alarmId: number;
  limit: number;
  value: number;
  AlarmType: AlarmType;
  priority: number;
  timestamp: Date;
}
