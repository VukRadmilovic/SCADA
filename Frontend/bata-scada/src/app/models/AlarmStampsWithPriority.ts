import {AlarmType} from "./enums/AlarmType";

export interface AlarmStampsWithPriority {
  alarmId: number;
  limit: number;
  value: number;
  priority: AlarmType;
  timestamp: Date;
}
