import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-signup-steps',
  templateUrl: './signup-steps.component.html',
  styleUrls: ['./signup-steps.component.css']
})
export class SignupStepsComponent {
  email: string = "";
  password: string = "";
  confirmPassword: string = "";
  nickname: string = "";
  country: string = "";
  city: string = "";
  phone_number: string = "";
  showStepInitial: boolean = true;
  showStep1: boolean = false;
  showStep2: boolean = false;
  showStep3: boolean = false;
  errorMessage: string = "";

  constructor(private router: Router, private authService: AuthService) { }

  step_initial_action() {
    if(this.confirmPassword == this.password){
    this.showStepInitial = false;
    this.showStep1 = true;
    }
  }
  step1_action() {
    this.showStep1 = false;
    this.showStep2 = true;
  }
  step2_action() {
    this.showStep2 = false;
    this.showStep3 = true;
  }
  step3_action() {
    // Register function
    const user = {
      Email: this.email,
      Password: this.password,
      Username: this.nickname,
      Location: `${this.city}, ${this.country}`, 
      PhoneNumber: this.phone_number
    };
  
    this.authService.register(user).subscribe(
      response => {
        // Registration successful
        console.log('Registration successful');
        // You can perform additional actions here if needed
  
        // Navigate to the home route after successful registration
        this.router.navigate(['/home']);
      },
      error => {
        // Handle registration errors
        console.error('Registration failed:', error.error);
        this.errorMessage = 'Registration failed. Please check your input and try again.';
      }
    );
  }

  navigateToLogIn() {
    this.router.navigate(['/login']); // Navigate to the login route
  }
}
