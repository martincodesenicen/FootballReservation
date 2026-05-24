import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, switchMap, map } from 'rxjs';
import { AuthResponse, LoginDto, RegisterDto, UserProfile } from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  // Ajusta el puerto base si tu backend corre en otro (ej: 7134, 5001)
  private baseUrl = 'http://localhost:5035/api'; 

  login(credentials: LoginDto): Observable<UserProfile> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/auth/login`, credentials).pipe(
      tap(response => localStorage.setItem('token', response.token)),
      // Una vez guardado el token, el interceptor lo leerá para la siguiente petición:
      switchMap(() => this.getUserProfile()) 
    );
  }

  register(userData: RegisterDto): Observable<UserProfile> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/auth/register`, userData).pipe(
      tap(response => localStorage.setItem('token', response.token)),
      switchMap(() => this.getUserProfile())
    );
  }

  getUserProfile(): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.baseUrl}/users/me`).pipe(
      tap(profile => localStorage.setItem('user', JSON.stringify(profile)))
    );
  }

  logout(): void {
    localStorage.clear();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUser(): UserProfile | null {
    const userJson = localStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : null;
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}