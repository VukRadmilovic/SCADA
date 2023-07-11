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
    const alarmId = ((<Element>$event.target).parentElement as Element).id.slice(1);
    this.alarmService.snooze(alarmId).subscribe({
      next : (results) => {
        this.notificationService.createNotification("Alarm snoozed");
        this.refresh();
      },
      error : (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    });
  }

  ngOnInit(): void {
    setInterval(() => {
      this.refresh();
    }, 1000);
  }

  private refresh() {
    this.alarmService.getActive().subscribe({
      next: (results) => {
        this.alarms = results;
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
