export interface LoginDto {
  email: string;
  password:  string;
}

export interface RegisterDto {
  firstName: string;
  lastName: string;
  email: string;
  password:  string;
}

export interface AuthResponse {
  token: string;
}

export interface UserProfile {
  message: string;
  userId: string;
  email: string;
  role: string;
}