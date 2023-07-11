import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {TagService} from "../../services/tag.service";
import {ValueDTO} from "../../models/ValueDTO";
import {InputTagDTO} from "../../models/InputTagDTO";
import {DigitalInput} from "../../models/DigitalInput";
import {DriverType} from "../../models/enums/DriverType";
import {AnalogInputDTO} from "../../models/AnalogInputDTO";
import {NotificationsService} from "../../services/notifications.service";
import {Alarm} from "../../models/Alarm";
import {AlarmType} from "../../models/enums/AlarmType";
import {AlarmDTO} from "../../models/AlarmDTO";

@Component({
  selector: 'app-input-tags',
  templateUrl: './input-tags.component.html',
  styleUrls: ['./input-tags.component.css','../../../styles.css']
})
export class InputTagsComponent implements OnInit{

  inputTags : InputTagDTO[] = [];
  inputTagValues : boolean[] = [];
  types = ["Analog","Digital"];
  drivers = ["Rtu","Simulation"];
  alarmTypes = ["High", "Low"];
  priorities = [1, 2, 3];
  addresses = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20];
  isDigitalChosen = true;
  tagCreationForm! : FormGroup;
  showForm = false;
  modifiedValues : any = {};
  limit: any;
  alarmCreationForm: FormGroup[] = [];
  constructor(private tagService: TagService,
              private notificationService : NotificationsService,
              private formBuilder: FormBuilder,) {}
  ngOnInit() {
    this.tagCreationForm = this.formBuilder.group({
      tagType: new FormControl(null, [Validators.required]),
      tagName: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      address: new FormControl(null, [Validators.required]),
      driver: new FormControl(null, [Validators.required]),
      scanTime: new FormControl(null, [Validators.required]),
      isOn : new FormControl(true),
      lowLimit: new FormControl(null, [Validators.required]),
      highLimit: new FormControl(null, [Validators.required]),
      units: new FormControl(null, [Validators.required]),
    });

    this.tagService.getAllInputTags().subscribe({
      next : (results) => {
        this.inputTags = results;
        this.inputTags.forEach(() => {
          this.alarmCreationForm.push(
            this.formBuilder.group({
              type: new FormControl(null, [Validators.required]),
              priority: new FormControl(null, [Validators.required]),
              limit: new FormControl(null, [Validators.required])
            })
          )
        });
      },
      error : (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    })

    // setInterval(() => {
    //   this.tagService.scan('testSim2').subscribe({
    //     next : (result) => {
    //       console.log(result);
    //     },
    //     error : (err) => {
    //       console.log(err);
    //     }
    //   })
    // }, 2000);
  }

  public typeChanged(value : any) : void {
    if(value == "Digital") {
      this.tagCreationForm.controls['lowLimit'].setValue(0);
      this.tagCreationForm.controls['highLimit'].setValue(0);
      this.tagCreationForm.controls['units'].setValue('x');
      this.isDigitalChosen = true;
    }
    else {
      this.isDigitalChosen = false;
      this.tagCreationForm.controls['lowLimit'].setValue(null);
      this.tagCreationForm.controls['highLimit'].setValue(null);
      this.tagCreationForm.controls['units'].setValue(null);
    }
  }

  public driverChanged(value : any) : void {
    if(value == "Simulation") {
      this.addresses = [18,19,20];
    }
    else {
      this.addresses = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17];
    }
  }

  public add() {
    if(this.tagCreationForm.valid) {
      if(this.isDigitalChosen) {
        const digitalInput : DigitalInput = {
          tagName : <string>this.tagCreationForm.controls['tagName'].value,
          description : <string>this.tagCreationForm.controls['description'].value,
          address : <number>this.tagCreationForm.controls['address'].value,
          driver : <DriverType>this.tagCreationForm.controls['driver'].value,
          isOn : <boolean>this.tagCreationForm.controls['isOn'].value,
          scanTime : <number>this.tagCreationForm.controls['scanTime'].value
        }
        this.tagService.addDigitalInputTag(digitalInput).subscribe({
          next : (result) => {
            window.location.reload();
          },
          error : (err) => {
            console.log(err);
            for (let key in err.error.errors) {
              this.notificationService.createNotification(err.error.errors[key]);
            }
          }
        })
      }
      else {
        const analogInput : AnalogInputDTO = {
          tagName : <string>this.tagCreationForm.controls['tagName'].value,
          description : <string>this.tagCreationForm.controls['description'].value,
          address : <number>this.tagCreationForm.controls['address'].value,
          driver : <DriverType>this.tagCreationForm.controls['driver'].value,
          lowLimit : <number>this.tagCreationForm.controls['lowLimit'].value,
          isOn : <boolean>this.tagCreationForm.controls['isOn'].value,
          scanTime : <number>this.tagCreationForm.controls['scanTime'].value,
          highLimit : <number>this.tagCreationForm.controls['highLimit'].value,
          units : <string>this.tagCreationForm.controls['units'].value,
        }
        this.tagService.addAnalogInputTag(analogInput).subscribe({
          next : (result) => {
            window.location.reload();
          },
          error : (err) => {
            console.log(err);
            for (let key in err.error.errors) {
              this.notificationService.createNotification(err.error.errors[key]);
            }
          }
        })
      }
    }
  }

  public showAddForm() : void {
    this.showForm = true;
  }

  public hideForm() : void {
    this.showForm = false;
  }

  public delete(event : any) : void {
    const tagName = ((event.target as Element).parentElement as Element).id;
    this.tagService.deleteTag(tagName).subscribe( {
      next : () => {
        window.location.reload();
      },
      error : (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    })
  }

  public changeValue(event: any) : void {
    const tagName = ((event.target as Element).parentElement as Element).id;
    const allCheckboxes = Array.prototype.slice.call(document.querySelectorAll('mat-checkbox'));
    let value = false;
    for(let checkbox of allCheckboxes) {
      if(checkbox.id == tagName) {
        if(checkbox.classList.contains('mat-mdc-checkbox-checked')) value = true;
        break;
      }
    }
    if(value) {
      this.tagService.turnOnScan(tagName).subscribe( {
        next : () => {
          window.location.reload();
        },
        error : (err) => {
          console.log(err);
          for (let key in err.error.errors) {
            this.notificationService.createNotification(err.error.errors[key]);
          }
        }
      })
    }
    else {
      this.tagService.turnOffScan(tagName).subscribe( {
        next : () => {
          window.location.reload();
        },
        error : (err) => {
          console.log(err);
          for (let key in err.error.errors) {
            this.notificationService.createNotification(err.error.errors[key]);
          }
        }
      })
    }
  }

  newAlarm(index: number) {
    const form = this.alarmCreationForm[index]
    console.log(form.value + ', ' + this.inputTags[index].tagName);
    if (form.valid && this.isNumber(this.alarmCreationForm[index].controls['limit'].value)) {
      console.log('valid');
      const alarm : AlarmDTO = {
        type: <AlarmType>form.controls['type'].value,
        limit: <number>form.controls['limit'].value,
        priority: <number>form.controls['priority'].value
      };
      this.tagService.addAlarm(this.inputTags[index].tagName, alarm).subscribe({
        next: (result) => {
          window.location.reload();
        },
        error: (err) => {
          console.log(err);
          for (let key in err.error.errors) {
            this.notificationService.createNotification(err.error.errors[key]);
          }
        }
        }
      )
    }
  }

  isNumber(value?: string | number): boolean
  {
    return ((value != null) &&
      (value !== '') &&
      !isNaN(Number(value.toString())));
  }
}
