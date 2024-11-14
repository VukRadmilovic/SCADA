import {Component, OnInit} from '@angular/core';
import {InputTagDTO} from "../../models/InputTagDTO";
import {FormBuilder} from "@angular/forms";
import {TagService} from "../../services/tag.service";
import {NotificationsService} from "../../services/notifications.service";
import {DriverType} from "../../models/enums/DriverType";

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css']
})
export class TrendingComponent implements OnInit {
  inputTags: InputTagDTO[] = [];
  inputTagValues: boolean[] = [];
  types = ["Analog", "Digital"];
  drivers = ["Rtu", "Simulation"];
  addresses = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];
  isDigitalChosen = true;

  constructor(private tagService: TagService,
              private notificationService: NotificationsService,
              private formBuilder: FormBuilder,) {
  }

  ngOnInit() {
    this.tagService.getAllInputTags().subscribe({
      next: (results) => {
        results.forEach(tag =>{
         if (tag.isOn) this.inputTags.push(tag);
        })
        this.updateTagValues();
      },
      error: (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    })
  }

  async updateTagValues(){
      await Promise.all(this.inputTags.map(async (tag) => {
        while (true){
          if(tag.driver == DriverType.Simulation){
            this.tagService.scan(tag.tagName).subscribe({
              next: (results) => {
                console.log(tag.tagName + " " + results)
                if (typeof results === "number") {
                  tag.value = results
                }
              },
              error: (err) => {
                console.log(err);
                for (let key in err.error.errors) {
                  this.notificationService.createNotification(err.error.errors[key]);
                }
              }
            })
          }
          else{
            this.tagService.tagLastValue(tag.tagName).subscribe({
              next: (results) => {
                console.log(tag.tagName + " " + results)
                if (typeof results === "number") {
                  tag.value = results
                }
              },
              error: (err) => {
                console.log(err);
                for (let key in err.error.errors) {
                  this.notificationService.createNotification(err.error.errors[key]);
                }
              }
            })
          }
          await this.sleep(tag.scanTime)
        }
      }))
  }

  sleep (ms: number){
    return new Promise(resolve => setTimeout(resolve, ms))
  }
}
