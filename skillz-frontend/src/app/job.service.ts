// job.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiUrl = 'https://localhost:7062/job/Job'; // assuming the JobController route

  constructor(private http: HttpClient) {}

  getJobById(jobId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${jobId}`);
  }

  getAllJobs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/all`);
  }

  getJobsByTitle(jobTitle: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/title/${jobTitle}`);
  }

  getJobsByUser(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }

  getJobsByExperience(experiencedYears: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/experience/${experiencedYears}`);
  }

  createJob(jobDto: any, images: File[]): Observable<any> {
    const formData = new FormData();
  
    // Append jobDto properties to the FormData
    Object.keys(jobDto).forEach(key => {
      if (key === 'Images') {
        // Append images directly under the 'Images' property
        images.forEach((image, index) => {
          formData.append(`Images[${index}]`, image, image.name);
        });
      } else {
        formData.append(key, jobDto[key]);
      }
    });
  
    // Headers for handling FormData
    const headers = {
      // You can add any specific headers here if needed
    };
  
    return this.http.post<any>(`${this.apiUrl}`, formData, { headers }).pipe(
      catchError((error) => {
        console.error('Error in createJob:', error);
        return throwError(error);
      })
    );
  }
  

  updateJob(jobId: number, jobDto: any, images: File[]): Observable<any> {
    const formData = new FormData();
  
    // Append jobDto properties to the FormData
    Object.keys(jobDto).forEach(key => {
      if (key === 'Images') {
        // Append images directly under the 'Images' property
        images.forEach((image, index) => {
          formData.append(`Images[${index}]`, image, image.name);
        });
      } else {
        formData.append(key, jobDto[key]);
      }
    });
  
    // Headers for handling FormData
    const headers = new HttpHeaders();
  
    return this.http.put<any>(`${this.apiUrl}/${jobId}`, formData, { headers });
  }
  
  createJobWithImages(formData: FormData): Observable<any> {
    const headers = {
      // You can add any specific headers here if needed
    };

    return this.http.post(`${this.apiUrl}`, formData, {headers})
    .pipe(
      catchError((error) => {
        console.error('Error in createJob:', error);
        return throwError(error);
      })
    );
  }

  deleteJob(jobId: number): Observable<any> {
    const headers = new HttpHeaders({
    });

    return this.http.delete<any>(`${this.apiUrl}/${jobId}`, { headers });
  }

  getJobImagesById(jobId: number): Observable<Blob> {
    const url = `https://localhost:7062/job/Job/${jobId}/images`;
  
    // Specify responseType as 'blob' to handle binary data
    return this.http.get(url, { responseType: 'blob' });
  }

  filterJobs(jobTitle: string, date: string, location: string): Observable<any[]> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
  
    const params = new HttpParams()
      .set('jobTitle', jobTitle)  // Use empty string if jobTitle is null
      .set('date', date)   // Use empty string if date is null or not a valid Date object
      .set('location', location); // Use empty string if location is null
  
    return this.http.get<any[]>(`${this.apiUrl}/filter`, { headers, params })
      .pipe(
        catchError((error) => {
          console.error('Error filtering jobs list:', error);
          return throwError(error);
        })
      );
  }

}
