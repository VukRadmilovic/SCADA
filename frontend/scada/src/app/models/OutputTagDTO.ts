import {TagType} from "./enums/TagType";

export interface OutputTagDTO {
  tagName : string,
  description : string,
  address : number,
  initialValue : number | boolean,
  lowLimit : number,
  highLimit : number,
  units : string,
  type : TagType,
  value : number | boolean
}
