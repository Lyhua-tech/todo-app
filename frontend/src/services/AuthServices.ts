import axios from "axios";
import { RegisterRequest, LoginRequest, AuthRequest } from "../types/auth";

const API_URL = "http://localhost:5068/api";

class AuthService {
  async register(registerRequest: RegisterRequest): Promise<AuthRequest> {
    const response = await axios.post<AuthRequest>(
      `${API_URL}/auth/register`,
      registerRequest
    );

    if (response.data.token) {
      localStorage.setItem("user", JSON.stringify(response.data));
    }

    return response.data;
  }

  async login(loginRequest: LoginRequest): Promise<AuthRequest> {
    const response = await axios.post(`${API_URL}/auth/login`, loginRequest);

    if (response.data.token) {
      localStorage.setItem("user", JSON.stringify(response.data));
    }

    return response.data;
  }

  logout(): void {
    localStorage.removeItem("user");
  }

  getCurrentUser(): AuthRequest | null {
    const userStr = localStorage.getItem("user");

    if (userStr) {
      return JSON.parse(userStr);
    }

    return null;
  }

  getAuthHeader(): { Authorization: string } | {} {
    const user = this.getCurrentUser();

    if (user && user.token) {
      return { Authorization: `Bearer ${user.token}` };
    }

    return {};
  }

  isAuthenticated(): boolean {
    const user = this.getCurrentUser();
    return user !== null;
  }

  // Make sure the current user object contains the userId
  getUserId(): string | null {
    const user = this.getCurrentUser();
    return user ? user.userId : null; // Get userId from localStorage
  }
}

export default new AuthService();
