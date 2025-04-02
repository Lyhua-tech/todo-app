export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface AuthRequest {
  token: string;
  userId: string;
  username: string;
}

export interface User {
  userId: string;
  username: string;
  email: string;
}
