import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {OutputTagDTO} from "../../models/OutputTagDTO";
import {TagService} from "../../services/tag.service";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {TagType} from "../../models/enums/TagType";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {DigitalOutput} from "../../models/DigitalOutput";
import {AnalogOutput} from "../../models/AnalogOutput";
import {ValueDTO} from "../../models/ValueDTO";
import {NotificationsService} from "../../services/notifications.service";

@Component({
  selector: 'app-db-manager',
  templateUrl: './db-manager.component.html',
  styleUrls: ['./db-manager.component.css','../../../styles.css']
})
export class DbManagerComponent implements OnInit{
  outputTags : OutputTagDTO[] = [];
  outputTagValues : boolean[] = [];
  types = ["Analog","Digital"];
  addresses = [21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40];
  isDigitalChosen = true;
  tagCreationForm! : FormGroup;
  showForm = false;
  modifiedValues : any = {};
  constructor(private tagService: TagService,
              private notificationService : NotificationsService,
              private formBuilder: FormBuilder,) {}
  ngOnInit() {
    this.tagCreationForm = this.formBuilder.group({
      tagType: new FormControl(null, [Validators.required]),
      tagName: new FormControl(null, [Validators.required]),
      address: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      initialValueBool: new FormControl(false),
      initialValueNum: new FormControl(null, [Validators.required]),
      lowLimit: new FormControl(null, [Validators.required]),
      highLimit: new FormControl(null, [Validators.required]),
      units: new FormControl(null, [Validators.required]),
    });

    this.tagService.getAllOutputTags().subscribe({
      next : (results) => {
        for (let result of results) {
          if(result.type == TagType.Digital) {
            result.initialValue == 0 ? result.initialValue = false : result.initialValue = true;
            result.value == 0 ? result.value = false : result.value = true;
            this.outputTagValues.push(result.value);
          }
          else {
            this.outputTagValues.push(false);
            this.modifiedValues[result.tagName] = (<string>(<unknown>result.value)).toString();
          }
        }
          this.outputTags = results;
      },
      error : (err) => {
        console.log(err);
        for (let key in err.error.errors) {
          this.notificationService.createNotification(err.error.errors[key]);
        }
    }
    })
  }

  public typeChanged(value : any) : void {
    if(value == "Digital") {
      this.tagCreationForm.controls['lowLimit'].setValue(0);
      this.tagCreationForm.controls['highLimit'].setValue(0);
      this.tagCreationForm.controls['units'].setValue('x');
      this.tagCreationForm.controls['initialValueNum'].setValue(0);
      this.isDigitalChosen = true;
    }
    else {
      this.isDigitalChosen = false;
      this.tagCreationForm.controls['lowLimit'].setValue(null);
      this.tagCreationForm.controls['highLimit'].setValue(null);
      this.tagCreationForm.controls['units'].setValue(null);
      this.tagCreationForm.controls['initialValueNum'].setValue(null);
    }
  }

  public add() {
      if(this.tagCreationForm.valid) {
        if(this.isDigitalChosen) {
          const digitalOutput : DigitalOutput = {
            tagName : <string>this.tagCreationForm.controls['tagName'].value,
            description : <string>this.tagCreationForm.controls['description'].value,
            address : <number>this.tagCreationForm.controls['address'].value,
            initialValue: <boolean>this.tagCreationForm.controls['initialValueBool'].value
          }
          this.tagService.addDigitalOutputTag(digitalOutput).subscribe({
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
          const analogOutput : AnalogOutput = {
            tagName : <string>this.tagCreationForm.controls['tagName'].value,
            description : <string>this.tagCreationForm.controls['description'].value,
            address : <number>this.tagCreationForm.controls['address'].value,
            initialValue: <number>this.tagCreationForm.controls['initialValueNum'].value,
            lowLimit : <number>this.tagCreationForm.controls['lowLimit'].value,
            highLimit : <number>this.tagCreationForm.controls['highLimit'].value,
            units : <string>this.tagCreationForm.controls['units'].value,
          }
          this.tagService.addAnalogOutputTag(analogOutput).subscribe({
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

  public modifiedValue(event: any) : void {
    const tagName = <string>event.target.id;
    console.log(typeof this.modifiedValues[tagName])
    if(event.inputType == "insertText") this.modifiedValues[tagName] += <string>event.data;
    else this.modifiedValues[tagName] = this.modifiedValues[tagName].slice(0,-1);
  }

  public changeValue(event: any) : void {
    const tagName = ((event.target as Element).parentElement as Element).id;
    const valueDTO : ValueDTO = {
      value : 0
    }
    for(let tag of this.outputTags) {
      if(tag.tagName == tagName) {
        if(tag.type == "Digital") {
          const allCheckboxes = Array.prototype.slice.call(document.querySelectorAll('mat-checkbox'));
          let value = false;
          for(let checkbox of allCheckboxes) {
            if(checkbox.id == tagName) {
              if(checkbox.classList.contains('mat-mdc-checkbox-checked')) value = true;
              valueDTO.value = value? 1: 0;
              break;
            }
          }
        }
        else {
          valueDTO.value = this.modifiedValues[tagName];
        }
        this.tagService.changeOutputTagValue(tagName,valueDTO).subscribe( {
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
  }
}
