// booking.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = 'https://localhost:7062/booking/Booking'; // assuming the BookingController route

  constructor(private http: HttpClient) {}

  getBookingById(bookingId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${bookingId}`);
  }

  getAllBookings(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/all`);
  }

  getBookingsByClient(clientId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/client/${clientId}`);
  }

  getBookingsByProvider(providerId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/provider/${providerId}`);
  }

  getBookingsByStatus(status: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/status/${status}`);
  }

  createBooking(bookingDto: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(`${this.apiUrl}`, bookingDto, { headers });
  }

  // updateBooking(bookingId: number, bookingDto: any): Observable<any> {
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //   });

  //   return this.http.put<any>(`${this.apiUrl}/${bookingId}`, bookingDto, { headers });
  // }

  updateBookingStatus(bookingId: string, newStatus: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Fetch the existing booking information
    return this.http.get<any>(`${this.apiUrl}/${bookingId}`).pipe(
      switchMap((existingBooking) => {
        // Extract only the necessary information for updating
        const updatePayload = {
          clientUserId: existingBooking.clientUserId,
          providerUserId: existingBooking.providerUserId,
          dateTime: existingBooking.dateTime,
          jobId: existingBooking.jobId,
          details: existingBooking.details,
          status: newStatus
        };

        console.log('Update Payload:', updatePayload);
        return this.http.put<any>(`${this.apiUrl}/${bookingId}`, updatePayload, { headers });
      })
    );
  }

  deleteBooking(bookingId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${bookingId}`);
  }
}
