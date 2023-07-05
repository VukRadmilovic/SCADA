import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {DbManagerComponent} from "./components/db-manager/db-manager.component";

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'dbManager', component : DbManagerComponent},
  {path: '', redirectTo:'/login', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
