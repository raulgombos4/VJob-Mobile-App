import { Component } from '@angular/core';
import { JobService } from '../job.service';
import { UserService } from '../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  userId: number =0;
  username: string = '';
  location: string = '';
  verified: boolean = false;
  profileImage: string = '';
  certificates: string[] = [];
  isPopupOpen = false;
  newCertificateType: string = '';
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

  constructor(private jobService: JobService,
              private userService: UserService, 
              private router: Router, 
              private authService: AuthService,
              private route: ActivatedRoute){}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const userId = params['userId'];
      this.loadData(userId);
    });
  }

  loadData(idUser: number){
    this.userService.getUserById(idUser).subscribe(async (user: any) => {
      this.userId = idUser;
      this.username = user.username;
      this.location = user.location;
      this.loadCertificates(idUser);
    });
  }

  loadCertificates(IdUser: number){
    this.userService.getUserCertificatesByUserId(IdUser).subscribe(certificates => {
      this.certificates = certificates.map(cert => cert.certificateType);
    });
  }

  openPopup() {
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
  }

  addCertificate() {
    const certificatUserDto = {
      CertificateType: this.newCertificateType,
      IdUser: this.userId
      // Add other properties or images as needed for your API
    };

    // Call the createCertificatUserWithImages method to add the certificate
    this.userService.createCertificatUserWithImages(certificatUserDto).subscribe(
      (response) => {
        console.log('Certificate added successfully:', response);
        // Optionally, you can update your local certificates array or perform other actions
        this.certificates.push(this.newCertificateType);
        // Close the popup after successfully adding the certificate
        this.closePopup();
      },
      (error) => {
        console.error('Error adding certificate:', error);
        // Handle error as needed
        // You may want to display an error message to the user or take other actions
      }
    );
  }
}
