import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {GlobalError} from "../models/GlobalError";
import {HttpClient} from "@angular/common/http";
import {environment} from "../environment";
import {LoginDTO} from "../models/LoginDTO";
import {SuccessMessage} from "../models/SuccessMessage";
import {UserDTO} from "../models/UserDTO";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  prefix = environment.apiURL + "users/";
  constructor(private http : HttpClient) { }

  public login(credentials : LoginDTO) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "login",credentials);
  }

  public logout(username : string) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "logout/" + username, null);
  }

  public getAll() : Observable<UserDTO[]> {
    return this.http.get<UserDTO[]>(this.prefix + "all");
  }

  public register(user : UserDTO) : Observable<GlobalError | SuccessMessage> {
    return this.http.post<GlobalError | SuccessMessage>(this.prefix + "register",user);
  }
}
