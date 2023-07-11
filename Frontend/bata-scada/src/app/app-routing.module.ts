import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {DbManagerComponent} from "./components/db-manager/db-manager.component";
import {InputTagsComponent} from "./components/input-tags/input-tags.component";
import {RegistrationComponent} from "./components/registration/registration.component";
import {TrendingComponent} from "./components/trending/trending.component";

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'trending', component: TrendingComponent},
  {path: 'dbManager', component : DbManagerComponent},
  {path: 'dbManagerInput', component: InputTagsComponent},
  {path: 'dbManagerUsers', component: RegistrationComponent},
  {path: '', redirectTo:'/login', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
