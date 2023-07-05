import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../services/user.service";
import {LoginDTO} from "../../models/LoginDTO";
import {Router} from "@angular/router";
import {NotificationsService} from "../../services/notifications.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm! : FormGroup;
  constructor(private formBuilder: FormBuilder,
              private router : Router,
              private notificationService : NotificationsService,
              private userService: UserService) {
    this.loginForm = this.formBuilder.group({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  public login() : void {
    if(!this.loginForm.valid) return;
    const credentials : LoginDTO = {
      username : <string>this.loginForm.value.username,
      password : <string>this.loginForm.value.password
    }
    this.userService.login(credentials).subscribe( {
      next : () => {
        sessionStorage.setItem("user",this.loginForm.value.username);
        this.router.navigate(['dbManager']);
      },
      error: (err) => {
        for (let key in err.error.errors) {
            this.notificationService.createNotification(err.error.errors[key]);
        }
      }
    })
  }
}
