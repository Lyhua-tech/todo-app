// src/stores/authStore.ts
import { ref, computed } from "vue";
import { defineStore } from "pinia";
import authService from "../services/AuthServices";
import { RegisterRequest, LoginRequest, AuthRequest } from "../types/auth";

export const useAuthStore = defineStore("auth", () => {
  const user = ref<AuthRequest | null>(authService.getCurrentUser());
  const error = ref<string | null>(null);
  const loading = ref<boolean>(false);

  const isAuthenticated = computed(() => !!user.value);
  const username = computed(() => user.value?.username || "");
  const userId = computed(() => user.value?.userId || "");

  async function register(registerData: RegisterRequest) {
    try {
      loading.value = true;
      error.value = null;
      const response = await authService.register(registerData);
      user.value = response;
      return true;
    } catch (err: any) {
      error.value = err.response?.data?.message || "Registration failed";
      return false;
    } finally {
      loading.value = false;
    }
  }

  async function login(loginData: LoginRequest) {
    try {
      loading.value = true;
      error.value = null;
      const response = await authService.login(loginData);
      user.value = response;
      return true;
    } catch (err: any) {
      error.value = err.response?.data?.message || "Login failed";
      return false;
    } finally {
      loading.value = false;
    }
  }

  function logout() {
    authService.logout();
    user.value = null;
  }

  return {
    user,
    error,
    loading,
    isAuthenticated,
    username,
    userId,
    register,
    login,
    logout,
  };
});
