import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { JobService } from '../job.service';
import { UserService } from '../user.service';
import JSZip from 'jszip';

@Component({
  selector: 'app-job-page',
  templateUrl: './job-page.component.html',
  styleUrl: './job-page.component.css'
})
export class JobPageComponent implements OnInit{
  images:string[] = [
    'https://www.homelux.ro/blog/wp-content/uploads/2019/03/gradini-de-dimensiuni-mari8.jpg',
    'https://hips.hearstapps.com/hmg-prod/images/claude-monets-house-and-gardens-in-giverny-france-news-photo-1042013294-1562610151.jpg?crop=1.00xw:0.753xh;0,0.0671xh&resize=1200:*',
  ];

  //text
  certificates: string[] = [];
  jobId: number = 0;
  jobTitle: string = '';
  username: string = '';
  description: string = '';

  currentIndex: number = 0;
  isDropdownVisible: boolean = false;  // Added property

  constructor(
    private jobService: JobService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.params.subscribe(async params => {
      const jobId = params['jobId'];
      await this.loadPage(jobId);
    });
  }

  toggleDropdown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  nextImage() {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
    this.updateSlider();
  }
  
  prevImage() {
    this.currentIndex = (this.currentIndex - 1 + this.images.length) % this.images.length;
    this.updateSlider();
  }
  
  private updateSlider() {
    const sliderMask = document.querySelector('.w-slider-mask') as HTMLElement | null;
  
    if (sliderMask) {
      const translateValue = -100 * this.currentIndex + '%';
      sliderMask.style.transform = 'translateX(' + translateValue + ')';
    }
  }


loadPage(jobId: number) {
  this.jobService.getJobById(jobId).subscribe((job: any) => {
    this.userService.getUserById(job.idUser).subscribe(async (user: any) => {
      if (user) {
        console.log(job);
        console.log(user);
        this.jobId = jobId;
        this.description = job.description;
        this.jobTitle = job.jobTitle;
        this.username = user.username;
        this.images = await this.getJobBackgroundImages(job.idJob);
        this.loadCertificates(job.idUser);
      }
    });
  });
}

loadCertificates(IdUser: number){
  this.userService.getUserCertificatesByUserId(IdUser).subscribe(certificates => {
    this.certificates = certificates.map(cert => cert.certificateType);
  });
}

async getJobBackgroundImages(jobId: number) {
  console.log('Start getJobBackgroundImages');
  console.log('Job ID:', jobId);

  try {
    const response = await this.jobService.getJobImagesById(jobId).toPromise();
    console.log('API Response:', response);

    if (response instanceof Blob) {
      // Extract images from the ZIP file
      const zip = new JSZip();
      const zipData = await zip.loadAsync(response);

      const imageFiles = Object.values(zipData.files);
      const imageUrls: string[] = [];

      for (const file of imageFiles) {
        const imageUrl = URL.createObjectURL(await file.async('blob'));
        imageUrls.push(imageUrl);
      }

      if (imageUrls.length > 0) {
        console.log('Image URLs:', imageUrls);
        return imageUrls;
      } else {
        console.error('No images found in the ZIP file.');
      }
    } else {
      console.error('Unexpected response format:', response);
    }
  } catch (error) {
    console.error('Error fetching images:', error);
  }

  // Return default path if an error occurred
  console.log('Returning default paths.');
  return ['path/to/default/image.jpg'];
}

redirectToBookForm() {
    this.router.navigate(['/booking-form', this.jobId]);
}
}

