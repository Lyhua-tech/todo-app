<!-- src/App.vue -->
<template>
  <div id="app">
    <router-view />
  </div>
</template>

<script setup lang="ts">
import { onMounted } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "./stores/authStore";

const router = useRouter();
const authStore = useAuthStore();

onMounted(() => {
  // Check if the user is already authenticated when the app loads
  if (
    !authStore.isAuthenticated &&
    router.currentRoute.value.meta.requiresAuth
  ) {
    router.push("/login");
  }
});
</script>

<style>
#app {
  font-family: "Inter", "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  min-height: 100vh;
}
</style>
