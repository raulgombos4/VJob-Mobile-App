// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { SignupStepsComponent } from './signup-steps/signup-steps.component';
import { AuthService } from './auth.service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { JobsListingComponent } from './jobs-listing/jobs-listing.component';
import { JobPageComponent } from './job-page/job-page.component';
import { MatIconModule } from '@angular/material/icon';
import { JobPostComponent } from './job-post/job-post.component';
import { AppointmentPageComponent } from './appointment-page/appointment-page.component';
import { HistoryPageComponent } from './history-page/history-page.component';
import { MyJobsComponent } from './my-jobs/my-jobs.component';
import { BookFormComponent } from './book-form/book-form.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupStepsComponent,
    HomeComponent,
    ProfileComponent,
    JobsListingComponent,
    JobPageComponent,
    JobPostComponent,
    AppointmentPageComponent,
    HistoryPageComponent,
    MyJobsComponent,
    BookFormComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    AppRoutingModule,  // Add this line to include the routing module
    MatIconModule, NoopAnimationsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatNativeDateModule,
  ],
  providers: [
    AuthService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
