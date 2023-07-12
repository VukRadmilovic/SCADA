import {TagType} from "./enums/TagType";

export interface TagValueWithoutNameDTO {
  address: number;
  value: number;
  valueType: TagType;
  timestamp: Date;
}
