// api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost.com';

  constructor(private http: HttpClient) {}

  // Example method to fetch data from the API
  getData(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/your-endpoint`);
  }

  // Add more methods for other API calls as needed
}
