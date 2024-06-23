import { Component, OnInit } from '@angular/core';
import { JobService } from '../job.service';
import { UserService } from '../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import JSZip from 'jszip';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-my-jobs',
  templateUrl: './my-jobs.component.html',
  styleUrl: './my-jobs.component.css'
})
export class MyJobsComponent implements OnInit {
  jobs: {
    jobId: number;
    title: string;
    rating: string;
    username: string;
    workerId: number;
    experience: number;
    certificationStatus: boolean;
    isVerified: boolean;
    profileImage: string;
    jobTypeLogo: string;
    backgroundImage: string;
  }[] = [];
  isDropdownVisible: boolean = false;  // Added property
  imageFiles: File[] = [];
  
  constructor(
    private jobService: JobService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}
  
  toggleDropdown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }
   
  
  ngOnInit(): void {
    const idUser = this.authService.getUserId();
    if(idUser){
    this.loadJobsList(idUser);
    }
  }

  async loadJobsList(idUser: number): Promise<void> {
    this.jobService.getJobsByUser(idUser).subscribe(
      async jobs => {
        const jobPromises = jobs.map(async job => {
          const worker = await this.userService.getUserById(job.idUser).toPromise();

          return {
            jobId: job.idJob,
            title: job.jobTitle,
            rating: '4.3/5', // You can set the rating based on your logic
            username: worker.username,
            workerId: job.idUser,
            experience: job.experiencedYears,
            certificationStatus: await this.isCertified(job.idUser, job.jobTitle), // You need to fetch this information
            isVerified: false, // You need to fetch this information
            profileImage: worker.profileImage, // You need to fetch this information
            jobTypeLogo: '',
            backgroundImage: await this.getJobBackgroundImage(job.idJob)
          };
        });

        // Wait for all promises to resolve and then assign the values to this.jobs
        this.jobs = await Promise.all(jobPromises);
      },
      error => {
        console.error('Error loading jobs list:', error);
      }
    );
  }

  async isCertified(userId: number, jobTitle: string): Promise<boolean> {
    try {
      // Get the user's certificates
      const certificates = await this.userService.getUserCertificatesByUserId(userId).toPromise();
  
      if (certificates) {
        const isCertified = certificates.some((certificate) => certificate.certificateType === jobTitle);
        return isCertified;
      } else {
        // Handle the case where certificates are undefined
        return false;
      }
    } catch (error) {
      console.error('Error checking certification:', error);
      // Handle error as needed, e.g., return false or throw an exception
      return false;
    }
  }

  async getJobBackgroundImage(jobId: number) {
    console.log('Start getJobBackgroundImage');
    console.log('Job ID:', jobId);
  
    try {
      const response = await this.jobService.getJobImagesById(jobId).toPromise();
      console.log('API Response:', response);
  
      if (response instanceof Blob) {
        // Extract images from the ZIP file
        const zip = new JSZip();
        const zipData = await zip.loadAsync(response);
  
        // Assuming images are stored in the root of the ZIP file
        const imageFiles = Object.values(zipData.files);
  
        if (imageFiles.length > 0) {
          const imageUrl = URL.createObjectURL(await imageFiles[0].async('blob'));
          console.log('Image URL:', imageUrl);
          return imageUrl;
        } else {
          console.error('No image found in the ZIP file.');
        }
      } else {
        console.error('Unexpected response format:', response);
      }
    } catch (error) {
      console.error('Error fetching images:', error);
    }
  
    // Return default path if an error occurred
    console.log('Returning default path.');
    return 'path/to/default/image.jpg';
  }

  redirectToJobPage(jobId: number) {
    if(jobId) {
      this.router.navigate(['/job-page', jobId]);
    }
  }
}