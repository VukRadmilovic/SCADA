import {Component, OnInit, ViewChild} from '@angular/core';
import {TagService} from "../../services/tag.service";
import {NotificationsService} from "../../services/notifications.service";
import {FormBuilder} from "@angular/forms";
import {TagValueWithTimestamp} from "../../models/TagValueWithTimestamp";
import {AlarmStamps} from "../../models/AlarmStamps"
import {AlarmStampsWithPriority} from "../../models/AlarmStampsWithPriority";
import {AlarmType} from "../../models/enums/AlarmType";
import {TagValueWithoutNameDTO} from "../../models/TagValueWithoutNameDTO";
import {TagType} from "../../models/enums/TagType";
import {MatTable, MatTableDataSource} from "@angular/material/table";

const ELEMENT_DATA: TagValueWithTimestamp[] = [
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-22')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-25')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-26')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-27')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-28')},
];

const ELEMENT_DATA6: TagValueWithoutNameDTO[] = [
  {address: 5, value: 1.0079, valueType: TagType.Analog, timestamp: new Date('2024-07-22')},
  {address: 5, value: 1.0079, valueType: TagType.Analog, timestamp: new Date('2024-07-22')},
  {address: 5, value: 1.0079, valueType: TagType.Analog, timestamp: new Date('2024-07-22')},
  {address: 5, value: 1.0079, valueType: TagType.Analog, timestamp: new Date('2024-07-22')},
  {address: 5, value: 1.0079, valueType: TagType.Analog, timestamp: new Date('2024-07-22')},
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
  displayedColumns6: string[] = ['address', 'value', 'timestamp'];
  dataSource = ELEMENT_DATA;
  dataSource1 = ELEMENT_DATA1;
  dataSource2 = ELEMENT_DATA2;
  dataSource3 = ELEMENT_DATA;
  dataSource4 = new MatTableDataSource<TagValueWithTimestamp>();
  dataSource5 = new MatTableDataSource<TagValueWithTimestamp>();
  dataSource6 = new MatTableDataSource<TagValueWithoutNameDTO>();
  addresses: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];

  constructor(private tagService: TagService,
              private notificationService: NotificationsService,
              private formBuilder: FormBuilder,) {
  }

  ngOnInit() {
    this.dataSource6.data = [];
    this.dataSource5.data = [];
    this.tagService.tagAllValues("1").subscribe({
      next: (results) => {
        results.forEach(tag =>{
          this.dataSource6.data.push(tag);
        })
        console.log(this.dataSource6);
      },
      error: (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    });
    this.tagService.digitalAllValues().subscribe({
      next: (results) => {
        results.forEach(tag =>{
          this.dataSource5.data.push(tag);
        })
        console.log(this.dataSource5);
      },
      error: (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    });

    this.tagService.analogAllValues().subscribe({
      next: (results) => {
        results.forEach(tag =>{
          this.dataSource4.data.push(tag);
        })
        console.log(this.dataSource4);
      },
      error: (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    });
  }

  onChange6(value: any) {
    console.log(value.target.value);
  }
}
