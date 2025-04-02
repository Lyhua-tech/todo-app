<template>
  <div class="min-h-screen bg-gray-100">
    <nav class="bg-white shadow">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between h-16">
          <div class="flex">
            <div class="flex-shrink-0 flex items-center">
              <h1 class="text-xl font-bold text-pink-700">MyApp</h1>
            </div>
          </div>
          <div class="flex items-center">
            <div class="ml-3 relative">
              <div class="flex items-center space-x-4">
                <span class="text-pink-700"
                  >Welcome, {{ authStore.username }}</span
                >
                <button
                  @click="handleLogout"
                  class="text-sm font-medium text-indigo-600 hover:text-indigo-500"
                >
                  Sign out
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </nav>

    <div class="py-10">
      <header>
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <h1 class="text-3xl font-bold leading-tight text-pink-600">
            Dashboard
          </h1>
        </div>
      </header>
      <main>
        <div>
          <div
            class="text-pink-600 max-w-[1600px] mx-auto flex flex-col items-center mt-20"
          >
            <h1 class="text-xl font-bold">ToDo Application</h1>
            <TodoList :todos="todos" @refresh="fetchTodos" />
            <NewTask @refresh="fetchTodos" />
            <h1
              class="text-2xl font-bold text-start text-pink-600 ml-3 mb-3 my-2"
            >
              History
            </h1>
          </div>
          <section class="max-w-[600px] mx-auto w-full">
            <div
              class="border-2 border-stone-300 rounded-lg text-stone-500 flex flex-col items-center justify-center"
            >
              <TaskHistory :period="period[0]" />
              <div
                class="bg-stone-300 max-w-[580px] h-[2px] rounded w-full"
              ></div>
              <TaskHistory :period="period[1]" />
            </div>
          </section>
        </div>
      </main>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useAuthStore } from "../stores/authStore";
import { useRouter } from "vue-router";
import axios from "axios";

import TodoList from "../components/TodoList.vue";
import TaskHistory from "../components/TaskHistory.vue";
import NewTask from "../components/NewTask.vue";

export default defineComponent({
  components: { TodoList, TaskHistory, NewTask },
  data() {
    return {
      todos: [] as Array<{ id: number; title: string; isComplete: boolean }>,
      period: [1, 7] as number[],
      authStore: useAuthStore(),
      router: useRouter(),
    };
  },
  methods: {
    async fetchTodos() {
      try {
        const res = await axios.get("http://localhost:5068/api/todoitem");
        this.todos = res.data;
      } catch (error) {
        console.error("Error fetching todos:", error);
      }
    },
    handleLogout() {
      this.authStore.logout();
      this.router.push("/login");
    },
  },
  mounted() {
    this.fetchTodos();
  },
});
</script>
