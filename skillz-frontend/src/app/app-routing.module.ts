import { Component } from '@angular/core';
// app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupStepsComponent } from './signup-steps/signup-steps.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { JobsListingComponent  } from './jobs-listing/jobs-listing.component';
import { JobPageComponent } from './job-page/job-page.component';
import { JobPostComponent } from './job-post/job-post.component';
import { AppointmentPageComponent } from './appointment-page/appointment-page.component';
import { HistoryPageComponent } from './history-page/history-page.component';
import { MyJobsComponent } from './my-jobs/my-jobs.component';
import { BookFormComponent } from './book-form/book-form.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'signup-steps', component: SignupStepsComponent},
  { path: 'home', component:HomeComponent},
  { path: 'profile/:userId', component:ProfileComponent},
  { path: 'jobs-listing/:jobTitle', component:JobsListingComponent },
  { path: 'job-page/:jobId', component:JobPageComponent},
  { path: 'appointments', component:AppointmentPageComponent},
  { path: 'history', component:HistoryPageComponent},
  { path: 'job-post', component:JobPostComponent},
  { path: 'my-services', component:MyJobsComponent},
  { path: 'booking-form/:jobId', component:BookFormComponent},
  // Add other routes as needed
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
