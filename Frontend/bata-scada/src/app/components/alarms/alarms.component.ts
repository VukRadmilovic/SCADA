import {Component, OnInit} from '@angular/core';
import {NotificationsService} from "../../services/notifications.service";
import {AlarmService} from "../../services/alarm.service";
import {AlarmWithTagNameDTO} from "../../models/AlarmWithTagNameDTO";

@Component({
  selector: 'app-alarms',
  templateUrl: './alarms.component.html',
  styleUrls: ['./alarms.component.css', '../../../styles.css']
})
export class AlarmsComponent implements OnInit{

  alarms: AlarmWithTagNameDTO[] = []
  constructor(private notificationService : NotificationsService,
              private alarmService: AlarmService) {}
  snooze($event: MouseEvent) {

  }

  ngOnInit(): void {
    setInterval(() => {
      this.alarmService.getActive().subscribe({
        next : (results) => {
          this.alarms = results;
        },
        error : (err) => {
          console.log(err);
          for (let key in err.error.errors) {
            this.notificationService.createNotification(err.error.errors[key]);
          }
        }
      });
    }, 1000);
  }
}
