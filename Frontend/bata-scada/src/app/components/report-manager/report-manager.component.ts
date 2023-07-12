import {Component, OnInit} from '@angular/core';
import {TagService} from "../../services/tag.service";
import {NotificationsService} from "../../services/notifications.service";
import {FormBuilder} from "@angular/forms";
import {TagValueWithTimestamp} from "../../models/TagValueWithTimestamp";
import {AlarmStamps} from "../../models/AlarmStamps"
import {AlarmStampsWithPriority} from "../../models/AlarmStampsWithPriority";
import {AlarmType} from "../../models/enums/AlarmType";

const ELEMENT_DATA: TagValueWithTimestamp[] = [
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-22')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-25')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-26')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-27')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-28')},
];

const ELEMENT_DATA1: AlarmStamps[] =[
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22')},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22')},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22')},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22')}
]

const ELEMENT_DATA2: AlarmStampsWithPriority[] =[
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: AlarmType.High},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: AlarmType.High},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: AlarmType.High},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: AlarmType.High}
]

@Component({
  selector: 'app-report-manager',
  templateUrl: './report-manager.component.html',
  styleUrls: ['./report-manager.component.css']
})
export class ReportManagerComponent implements OnInit{
  displayedColumns: string[] = ['tagName', 'address', 'value', 'timestamp'];
  displayedColumns1: string[] = ['alarmId', 'limit', 'value', 'timestamp'];
  displayedColumns2: string[] = ['alarmId', 'limit', 'value', 'priority', 'timestamp'];
  dataSource = ELEMENT_DATA;
  dataSource1 = ELEMENT_DATA1;
  dataSource2 = ELEMENT_DATA2;
  dataSource3 = ELEMENT_DATA;
  dataSource4 = ELEMENT_DATA;
  dataSource5 = ELEMENT_DATA;
  dataSource6 = ELEMENT_DATA;

  constructor(private tagService: TagService,
              private notificationService: NotificationsService,
              private formBuilder: FormBuilder,) {
  }

  ngOnInit() {
    // this.dataSource.sort = this.sort;
  }
}
