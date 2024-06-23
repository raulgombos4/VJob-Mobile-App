// login.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    const user = {
      email: this.email, // Use email instead of username
      password: this.password
    };

    this.authService.login(user).subscribe(
      response => {
        // Login successful
        console.log('Login successful');
        // You can perform additional actions here if needed
        this.router.navigate(['/home']); // Navigate to the home route after successful login
      },
      error => {
        // Handle login errors
        console.error('Login failed:', error.error);
        this.handleLoginError(error.error);
      }
    );
  }

  private handleLoginError(error: any): void {
    // Handle specific errors and call functions based on error messages
    if (error === 'Invalid email') {
      // Call function for invalid email error
      this.handleInvalidEmail();
    } else if (error === 'Invalid password') {
      // Call function for invalid password error
      this.handleInvalidPassword();
    } else {
      // Handle other errors if needed
      this.errorMessage = 'An unexpected error occurred.';
    }
  }

  private handleInvalidEmail(): void {
    // Function to run when email is invalid
    console.log('Invalid email');
    this.errorMessage = 'Invalid email. Please check your email.';
  }

  private handleInvalidPassword(): void {
    // Function to run when password is invalid
    console.log('Invalid password');
    this.errorMessage = 'Invalid password. Please check your password.';
  }

  navigateToSignup() {
    this.router.navigate(['/signup-steps']); // Navigate to the signup route
  }
}
