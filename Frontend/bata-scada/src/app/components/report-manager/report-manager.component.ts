import {Component, OnInit, ViewChild} from '@angular/core';
import {TagService} from "../../services/tag.service";
import {NotificationsService} from "../../services/notifications.service";
import {FormBuilder} from "@angular/forms";
import {MatSort} from "@angular/material/sort";
import {TagValueWithTimestamp} from "../../models/TagValueWithTimestamp";

const ELEMENT_DATA: TagValueWithTimestamp[] = [
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-22')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-25')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-26')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-27')},
  {tagName: 'test', address: 1, value: 1.0079, timestamp: new Date('2024-07-28')},
];

@Component({
  selector: 'app-report-manager',
  templateUrl: './report-manager.component.html',
  styleUrls: ['./report-manager.component.css']
})
export class ReportManagerComponent implements OnInit{
  displayedColumns: string[] = ['tagName', 'address', 'value', 'timestamp'];
  dataSource = ELEMENT_DATA;
  dataSource1 = ELEMENT_DATA;
  dataSource2 = ELEMENT_DATA;
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
