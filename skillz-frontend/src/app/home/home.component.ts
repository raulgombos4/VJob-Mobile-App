import { Component, OnInit } from '@angular/core';
import { JobService } from '../job.service';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { UserService } from '../user.service';
import { BookingService } from '../booking.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  appointmentsClient: {
    bookingId: string;
    jobTitle: string;
    rating: string;
    clientUsername: string;
    status: string;
    profileImage: string;
    date: string;
  }[] = [];

  userId: number = 0;
  jobTitles: string[] = [];
  selectedJobTitle: string = '';
  isDropdownVisible: boolean = false;  // Added property

  constructor(private jobService: JobService, private router: Router, private authService: AuthService,  private bookingService: BookingService,
    private userService: UserService) {}

  ngOnInit() {
    const id = this.authService.getUserId()
    if(id)
    {
      this.userId = id;
    }
    this.loadJobTitles();
    this.loadAppointments()
  }

  loadJobTitles() {
    this.jobService.getAllJobs().subscribe(
      jobs => {
        if (jobs) {
          this.jobTitles = [...new Set(jobs.map(job => job.jobTitle))];
        } else {
          console.error('Received null or undefined jobs array.');
        }
      },
      error => {
        console.error('Error loading job titles:', error);
      }
    );
  }

  toggleDropdown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }
  routeToJobPost(){
    this.router.navigate(['/job-post']);
  }
  searchClicked() {
    if (this.selectedJobTitle) {
      this.router.navigate(['/jobs-listing', this.selectedJobTitle.toLowerCase()]);
    }
  }
  navigate(location: string) {
    this.router.navigate([location]);
  }

  redirectToProfile() {
    this.router.navigate(['/profile', this.userId]);
  }

  loadAppointments(): void {
    const userId = this.authService.getUserId();
    if (userId !== null) {
    this.bookingService.getBookingsByClient(userId).subscribe((bookings) => {
      this.populateAppointmentsClient(bookings);
    });
  }
  }

  populateAppointmentsClient(bookings: any[]): void {
    this.appointmentsClient = [];

    bookings.forEach((booking) => {
      if(booking.status != "1" && booking.status != "2"){
      const jobObservable = this.jobService.getJobById(booking.jobId);
      const providerObservable = this.userService.getUserById(booking.providerUserId);

      forkJoin([jobObservable, providerObservable]).subscribe(([job, provider]) => {
        this.appointmentsClient.push({
          bookingId: booking.bookingId,
          jobTitle: job.jobTitle,
          rating: '4.3/5',
          clientUsername: provider.username,
          status: booking.status,
          profileImage: "",
          date: new Date(booking.dateTime).toLocaleDateString(),
        });
      });
    }});

    console.log("Client View:", this.appointmentsClient);
  }
  statusText(status: any): { text: string, color: string } {
    let text = "";
    let color = "";
  
    if (status == "1") {
      text = "Pending";
      color = "orange"; // Set your desired color for Pending
    } else if (status == "2") {
      text = "Accepted";
      color = "green"; // Set your desired color for Accepted
    } else if (status == "3") {
      text = "Canceled";
      color = "red"; // Set your desired color for Canceled
    } else if (status == "4") {
      text = "Rejected";
      color = "red"; // Set your desired color for Rejected
    } else {
      text = "Completed";
      color = "#03a1fc"; // Set your desired color for Completed
    }
  
    return { text, color };
  }
}
