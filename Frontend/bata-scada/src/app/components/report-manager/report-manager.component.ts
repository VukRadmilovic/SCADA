import {Component, OnInit} from '@angular/core';
import {TagService} from "../../services/tag.service";
import {NotificationsService} from "../../services/notifications.service";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {TagValueWithTimestamp} from "../../models/TagValueWithTimestamp";
import {AlarmStamps} from "../../models/AlarmStamps"
import {AlarmStampsWithPriority} from "../../models/AlarmStampsWithPriority";
import {AlarmType} from "../../models/enums/AlarmType";
import {TagValueWithoutNameDTO} from "../../models/TagValueWithoutNameDTO";
import {TagType} from "../../models/enums/TagType";
import {MatTableDataSource} from "@angular/material/table";

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
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: 1, AlarmType: AlarmType.High},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: 3, AlarmType: AlarmType.High},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: 2, AlarmType: AlarmType.High},
  {alarmId: 1, limit: 100, value:110, timestamp: new Date('2024-07-22'), priority: 1, AlarmType: AlarmType.High}
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
  // dataSource1 = new MatTableDataSource<AlarmStampsWithPriority>();
  // dataSource2 = new MatTableDataSource<AlarmStampsWithPriority>();
  // dataSource3 = new MatTableDataSource<TagValueWithTimestamp>();
  // dataSource4 = new MatTableDataSource<TagValueWithTimestamp>();
  // dataSource5 = new MatTableDataSource<TagValueWithTimestamp>();
  // dataSource6 = new MatTableDataSource<TagValueWithoutNameDTO>();

  dataSource1 : AlarmStampsWithPriority[] = [];
  dataSource2 : AlarmStampsWithPriority[] = [];
  dataSource3 : TagValueWithTimestamp [] = [];
  dataSource4 : TagValueWithTimestamp [] = [];
  dataSource5 : TagValueWithTimestamp [] = [];
  dataSource6 : TagValueWithoutNameDTO [] = [];
  addresses: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];

  range3 = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  range1 = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  selected6: number = 1;
  priorities: string[] = ["1", "2", "3"];
  selected2: string = "1";

  constructor(private tagService: TagService,
              private notificationService: NotificationsService,
              private formBuilder: FormBuilder,) {
  }

  ngOnInit() {
    this.dataSource6 = [];
    this.dataSource5 = [];
    this.tagService.tagAllValues("1").subscribe({
      next: (results) => {
        this.dataSource6 = results;
        // results.forEach(tag =>{
        //   this.dataSource6.data.push(tag);
        // })
        //console.log(this.dataSource6);
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
        this.dataSource5 = results;
        // results.forEach(tag =>{
        //   this.dataSource5.data.push(tag);
        // })
        //console.log(this.dataSource5);
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
        this.dataSource4 = results;
        // results.forEach(tag =>{
        //   this.dataSource4.data.push(tag);
        // })
        //console.log(this.dataSource4);
      },
      error: (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    });
  }

  update1($event:any){
    this.dataSource1 = [];
    if(this.range1.value.start != null && this.range1.value.end != null){
      this.tagService.someTime(this.range1.value.start, this.range1.value.end).subscribe({
        next: (results) => {
          // results.forEach(tag =>{
          //   this.dataSource1.data.push(tag);
          // })
          this.dataSource1 = results;
          //console.log(this.dataSource1);
        },
        error: (err) => {
          console.log(err);
          for (let key in err.error.errors) {
            this.notificationService.createNotification(err.error.errors[key]);
          }
        }
      });
    }
  }

  update3($event:any){
    this.dataSource3 = [];
    if(this.range3.value.start != null && this.range3.value.end != null){
      this.tagService.allTime(this.range3.value.start, this.range3.value.end).subscribe({
        next: (results) => {
          // results.forEach(tag =>{
          //   this.dataSource3.data.push(tag);
          // })
          this.dataSource3 = results;
          //console.log(this.dataSource3);
        },
        error: (err) => {
          console.log(err);
          for (let key in err.error.errors) {
            this.notificationService.createNotification(err.error.errors[key]);
          }
        }
      });
    }
  }

  update2($event: any){
    this.dataSource2 = [];
    this.tagService.alarmWPriority(this.selected2).subscribe({
      next: (results) => {
        // results.forEach(tag =>{
        //   this.dataSource2.data.push(tag);
        // })
        this.dataSource2 = results;
        //console.log(this.dataSource2);
      },
      error: (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    });
  }

  update6($event: any) {
    this.dataSource6 = [];
    this.tagService.tagAllValues(this.selected6.toString()).subscribe({
      next: (results) => {
        // results.forEach(tag =>{
        //   this.dataSource6.data.push(tag);
        // })
        this.dataSource6 = results;
        //console.log(this.dataSource6);
      },
      error: (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    });
  }
}
