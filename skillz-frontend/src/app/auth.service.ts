// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthenticated() {
    return this.isAuth;
  }
  private apiUrl = 'https://localhost:7062/Authentication'; // backend url
  private tokenKey = 'auth_token'; // key to store the token in local storage
  private usernameKey = 'auth_username'; // key to store the username in local storage
  private emailKey = 'auth_email'; // key to store the email in local storage
  private userID= 'user_id';
  private isAuth = "false";
  constructor(private http: HttpClient) {}

  register(user: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(`${this.apiUrl}/register`, user, { headers })
    .pipe(
      tap(response => this.handleAuthentication(response))
    );
  }

  login(user: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, user)
      .pipe(
        tap(response => this.handleAuthentication(response))
      );
  }

  logout(): Observable<any> {
    // Implement logout logic here, if needed
    this.isAuth = "false";
    return this.http.post<any>(`${this.apiUrl}/logout`, {});
  }

  private handleAuthentication(response: any): void {
    console.log('Authentication response:', response);
    // Handle the authentication response, e.g., store the token, username, and email in local storage
    if (response && response.token) {
      localStorage.setItem(this.tokenKey, response.token);
    }

    if (response && response.username) {
      localStorage.setItem(this.usernameKey, response.username);
    }

    if (response && response.email) {
      localStorage.setItem(this.emailKey, response.email);
    }
    if (response && response.token) {
      localStorage.setItem(this.isAuth, "true");
      localStorage.setItem(this.tokenKey, response.token);
    }
    if (response && response.username) {
      localStorage.setItem(this.userID, response.userId);
    }
  }

  getToken(): string | null {
    // Retrieve the stored token from local storage
    return localStorage.getItem(this.tokenKey);
  }

  getUserId(): number | null {
    // Retrieve the stored token from local storage
    const userIdString = localStorage.getItem(this.userID);
  
    // Check if userIdString is null or undefined
    if (userIdString === null || userIdString === undefined) {
      return null;
    }
  
    // Parse the string to an integer
    const userId = parseInt(userIdString, 10);
  
    // Check if the parsing was successful
    if (isNaN(userId)) {
      return null; // Return null if parsing failed
    }
  
    return userId;
  }

  getUsername(): string | null {
    // Retrieve the stored username from local storage
    return localStorage.getItem(this.usernameKey);
  }

  getEmail(): string | null {
    // Retrieve the stored email from local storage
    return localStorage.getItem(this.emailKey);
  }
  getAuthStatus(): string | null {
    // Retrieve the stored email from local storage
    return localStorage.getItem(this.isAuth);
  }
}
