import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { BookingService } from '../booking.service';
import { UserService } from '../user.service';
import { JobService } from '../job.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-appointment-page',
  templateUrl: './appointment-page.component.html',
  styleUrl: './appointment-page.component.css'
})
export class AppointmentPageComponent implements OnInit {
  asClient: boolean = true;
  asWorker: boolean = false;
  circleColor: string = '#3498db';
  isDropdownVisible: boolean = false;  // Added property
  appointmentsClient: {
    bookingId: string;
    jobTitle: string;
    rating: string;
    clientUsername: string;
    status: string;
    profileImage: string;
    date: string;
  }[] = [];

  appointmentsWorker: {
    bookingId: string,
    jobTitle: string;
    rating: string;
    providerUsername: string;
    status: string;
    profileImage: string;
    date: string;
  }[] = [];

  constructor(
    private authService: AuthService,
    private bookingService: BookingService,
    private userService: UserService,
    private jobService: JobService
  ) {}

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments(): void {
    const userId = this.authService.getUserId();
    if (userId !== null) {
    this.bookingService.getBookingsByClient(userId).subscribe((bookings) => {
      this.populateAppointmentsClient(bookings);
    });
    
    this.bookingService.getBookingsByProvider(userId).subscribe((bookings) => {
      this.populateAppointmentsWorker(bookings);
    });
  }
  }

  populateAppointmentsClient(bookings: any[]): void {
    this.appointmentsClient = [];

    bookings.forEach((booking) => {
      if(booking.status == "1" || booking.status == "2"){
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

  populateAppointmentsWorker(bookings: any[]): void {
    this.appointmentsWorker = [];

    bookings.forEach((booking) => {
      if(booking.status == "1" || booking.status == "2"){
      const jobObservable = this.jobService.getJobById(booking.jobId);
      const clientObservable = this.userService.getUserById(booking.clientUserId);

      forkJoin([jobObservable, clientObservable]).subscribe(([job, client]) => {
        this.appointmentsWorker.push({
          bookingId: booking.bookingId,
          jobTitle: job.jobTitle,
          rating: '4.3/5',
          providerUsername: client.username,
          status: booking.status,
          profileImage: "",
          date: new Date(booking.dateTime).toLocaleDateString(),
        });
      });
    }});

    console.log("Worker View:", this.appointmentsWorker);
  }


updateBookingStatus(bookingId: string, newStatus: string): void {
  this.bookingService.updateBookingStatus(bookingId, newStatus).subscribe(() => {
    // Refresh the appointments after the status is updated
    this.loadAppointments();
  });
}

acceptHide(status:string){
  if(status == "1")
  {
    return true;
  }
  else return false;
}

completeHide(status:string){
  if(status == "2")
  {
    return true;
  }
  else return false;
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


// Initialize the active tab
openAsClient(){
  this.asClient=true;
  this.asWorker=false;
}

openAsWorker(){
  this.asClient=false;
  this.asWorker=true;
}

changeCircleColor(color: string): void {
  this.circleColor = color;
}

toggleDropdown() {
  this.isDropdownVisible = !this.isDropdownVisible;
}
}
