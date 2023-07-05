import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {NotificationsService} from "../../services/notifications.service";
import {UserService} from "../../services/user.service";
import {UserDTO} from "../../models/UserDTO";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css','../../../styles.css']
})
export class RegistrationComponent implements OnInit{

  registrationForm! : FormGroup;
  users : UserDTO[]  = [];
  displayedColumns: string[] = ['name', 'surname', 'username'];
  constructor(private formBuilder: FormBuilder,
              private router : Router,
              private notificationService : NotificationsService,
              private userService: UserService) {
    this.registrationForm = this.formBuilder.group({
      name: new FormControl('', [Validators.required]),
      surname: new FormControl('', [Validators.required]),
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.userService.getAll().subscribe({
      next : (result) => {
        this.users = result;
      }
    })
  }

  public register() : void {
    if(this.registrationForm.valid) {
      const user : UserDTO = {
        name : <string>this.registrationForm.controls['name'].value,
        surname : <string>this.registrationForm.controls['surname'].value,
        username : <string>this.registrationForm.controls['username'].value,
        password : <string>this.registrationForm.controls['password'].value,
      }
      console.log(user);
      this.userService.register(user).subscribe({
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
