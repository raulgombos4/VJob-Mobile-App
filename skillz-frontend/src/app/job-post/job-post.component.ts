import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { JobService } from '../job.service';


@Component({
  selector: 'app-job-post',
  templateUrl: './job-post.component.html',
  styleUrl: './job-post.component.css'
})
export class JobPostComponent {
  imageFiles: File[] = [];
  details: string = "";
  experiencedYears: string = "";
  selectedJobTitle: string = '';
  
  showStepInitial: boolean = true;
  showStep1: boolean = false;
  showStep2: boolean = false;
  showStep3: boolean = false;
  errorMessage: string = "";
  jobTitles: string[] = [
    "Accountant",
    "Babysitter",
    "Carpenter",
    "Dentist",
    "Electrician",
    "Florist",
    "Graphic Designer",
    "Hairdresser",
    "Interior Designer",
    "Janitor",
    "Landscaper",
    "Massage Therapist",
    "Nutritionist",
    "Optician",
    "Painter",
    "Quilter",
    "Realtor",
    "Swimming Instructor",
    "Tailor",
    "Upholsterer",
    "Veterinarian",
    "Wedding Planner",
    "X-ray Technician",
    "Yoga Instructor",
    "Zoologist"
  ];
  sanitizer: any;

  constructor(private router: Router, private authService: AuthService, private jobService: JobService) { }

  step_initial_action() {
    this.showStepInitial = false;
    this.showStep1 = true;
  }
  step1_action() {
    this.showStep1 = false;
    this.showStep2 = true;
  }
  step2_action() {
    this.showStep2 = false;
    this.showStep3 = true;
  }
  step3_action(): void {
    if (this.authService.getAuthStatus()) {
      const userId = this.authService.getUserId()
      if(userId){
      const formData = new FormData();
  
      formData.append('JobTitle', this.selectedJobTitle);
      formData.append('Description', this.details);
      formData.append('ExperiencedYears', this.experiencedYears.toString());
      formData.append('IdUser', userId.toString());
  
      for (const imageFile of this.imageFiles) {
        formData.append('Images', imageFile);
      }
  
      this.jobService.createJobWithImages(formData)
        .subscribe(
          (response) => this.handleJobCreationSuccess(response),
          (error) => this.handleJobCreationError(error)
        );
    } else {
      this.navigateToLogIn();
    }
  }
  }

  private handleJobCreationSuccess(response: any): void {
    console.log('Job created successfully!', response);
    this.router.navigate(['/home']);
  }

  private handleJobCreationError(error: any): void {
    console.error('Error creating job:', error);
    // Handle the error, update errorMessage, show a user-friendly message, etc.
  }

  navigateToLogIn(): void {
    this.router.navigate(['/login']);
  }

  handleImageUpload(event: any): void {
    const fileList: FileList | null = event.target.files;

    if (fileList) {
      this.imageFiles = Array.from(fileList);
    }
  }

  removeImage(index: number): void {
    this.imageFiles.splice(index, 1);
  }

  getPreviewImage(imageFile: File): string {
    return URL.createObjectURL(imageFile);
  }
}