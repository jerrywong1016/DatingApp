import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListComponent } from './list/list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children:[
      {path:'members', component:MemberDetailComponent, canActivate: [AuthGuard]},
      {path:'members/:id', component:HomeComponent, canActivate: [AuthGuard]},
      {path:'lists', component:ListComponent, canActivate: [AuthGuard]},
      {path:'messages', component:MessagesComponent, canActivate: [AuthGuard]},
    ]
  },   
  {path:'**', component:HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
