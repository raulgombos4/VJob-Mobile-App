import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { JobService } from '../job.service';
import { BookingService } from '../booking.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent implements OnInit{
  selectedDate: Date = new Date();
  // Define your list of dates (make sure they are in Date format)
  unavailableDates: Date[] = [new Date('2024-01-11'), new Date('2024-01-20')];
  username: string = '';
  jobTitle: string = '';
  jobId: number = 0;
  providerId: number = 0;
  details: string = "";
  location: string = "";

  showStepInitial: boolean = true;
  showStep1: boolean = false;
  errorMessage: string = "";
  selectedJobTitle: string = '';
  sanitizer: any;

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService, private jobService: JobService, private bookingService: BookingService, private userService: UserService) { }

  ngOnInit() {
    this.route.params.subscribe(async params => {
      const jobId = params['jobId'];
      await this.loadPage(jobId);
      await this.fetchProviderAppointments();
    });
  }

  async loadPage(jobId: number) {
    this.jobService.getJobById(jobId).subscribe((job: any) => {
      console.log("job: ",job);
      this.userService.getUserById(job.idUser).subscribe(async (user: any) => {
        if (user) {
          console.log(job);
          console.log(user);
          this.jobId = jobId;
          this.providerId = job.idUser;
          this.jobTitle = job.jobTitle;
          this.username = user.username;
        }
      });
    });
  }


  step_initial_action() {
    this.showStepInitial = false;
    this.showStep1 = true;
  }
  step1_action() {
     // Check if the user is authenticated
  if (this.authService.getAuthStatus() == "true") {
    // Combine details and location into a single string
    const combinedDetails = `${this.details} - ${this.location}`;

    // Create a booking object with the required properties
    const bookingDto = {
      clientUserId: this.authService.getUserId(), // Get client user id from auth service
      providerUserId: this.providerId, // Set provider user id to 1 (as mentioned)
      dateTime: this.selectedDate.toISOString(), // Convert selectedDate to ISO format
      details: combinedDetails,
      jobId: this.jobId,
      status: '1',
    };

    console.log(bookingDto);

    // Call the booking service to create the booking
    this.bookingService.createBooking(bookingDto).subscribe(
      (result) => {
        // Handle successful booking creation
        console.log('Booking created successfully:', result);

        // Fetch appointments for the provider and update unavailableDates
        this.fetchProviderAppointments();
        this.router.navigate(['/home']);
        // Optionally, navigate to a success page or perform other actions
      },
      (error) => {
        // Handle error during booking creation
        console.error('Error creating booking:', error);
      }

    );
  } else {
    console.log("Nu se auth is false");
  
    // Log token, user ID, username, and email from local storage
    console.log('Stored Token:', this.authService.getToken());
    console.log('Stored User ID:', this.authService.getUserId());
    console.log('Stored Username:', this.authService.getUsername());
    console.log('Stored Email:', this.authService.getEmail());
  }
  }

  async fetchProviderAppointments() {
    // Assume you have a function in your booking service to get provider's appointments
    this.bookingService.getBookingsByProvider(this.providerId).subscribe(
      (providerAppointments) => {
        console.log("Appointmaents: ",providerAppointments)
        // Filter appointments with status "accepted"
        const acceptedAppointments = providerAppointments.filter(appointment => appointment.status == 2);
        console.log("Accepted app:",acceptedAppointments)
        // Extract dates from acceptedAppointments and update unavailableDates
        const acceptedDates = acceptedAppointments.map(appointment => new Date(appointment.dateTime));
        console.log("Accepted dates:",acceptedDates)
        this.unavailableDates = [...this.unavailableDates, ...acceptedDates];
      },
      (error) => {
        console.error('Error fetching provider appointments:', error);
      }
    );
  }

  navigateToLogIn() {
    this.router.navigate(['/login']); // Navigate to the login route
  }

  dateSelected(date: Date): void {
    // Call your function with the selected date
    console.log('Selected date:', date);
    // You can call your function here and pass the selected date as a parameter
  }


  onDateSelected(event: any): void {
    this.selectedDate = event.value;
    this.callFunctionWithDate(this.selectedDate);
  }
  
  callFunctionWithDate(selectedDate: Date): void {
    console.log('Selected Date:', selectedDate.toISOString());
    // Your logic here
  }
  
  dateFilter = (date: Date | null): boolean => {
    // Disable dates in the unavailableDates array
    return !this.unavailableDates.some((unavailableDate) => this.isSameDay(date, unavailableDate));
  };

  private isSameDay(date1: Date | null, date2: Date | null): boolean {
    if (!date1 || !date2) {
      return false;
    }
    return date1.getDate() === date2.getDate() && date1.getMonth() === date2.getMonth() && date1.getFullYear() === date2.getFullYear();
  }
}