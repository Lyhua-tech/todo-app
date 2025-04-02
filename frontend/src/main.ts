// src/main.ts
import { createApp } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";
import router from "./router";
import axios from "axios";
import AuthServices from "./services/AuthServices";
import "./style.css";

// Configure axios default headers
axios.interceptors.request.use(
  (config) => {
    const user = AuthServices.getCurrentUser();
    if (user && user.token) {
      config.headers.Authorization = `Bearer ${user.token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

const app = createApp(App);

app.use(createPinia());
app.use(router);

app.mount("#app");
