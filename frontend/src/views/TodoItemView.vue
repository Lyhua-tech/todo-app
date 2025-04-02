<template>
  <div
    class="flex flex-col items-center justify-center min-h-screen bg-gray-100 p-6"
  >
    <div class="w-full max-w-2xl bg-white shadow-lg rounded-lg p-6">
      <h2 class="text-2xl font-bold text-gray-800 mb-4">Todo Details</h2>

      <div v-if="todoItem" class="space-y-4">
        <div class="border-b pb-2">
          <h3 class="text-xl font-semibold text-gray-700">
            {{ todoItem.title }}
          </h3>
          <p class="text-gray-600">{{ todoItem.content }}</p>
        </div>

        <div class="flex justify-between text-sm text-gray-500">
          <p>
            Created:
            <span class="font-medium">{{
              formatDate(todoItem.createdAt)
            }}</span>
          </p>
          <p>
            Completed:
            <span class="font-medium">{{
              formatDate(todoItem.completeAt)
            }}</span>
          </p>
        </div>

        <div :class="todoItem.isCompleted ? 'text-green-500' : 'text-red-500'">
          Status:
          <span class="font-medium">{{
            todoItem.isCompleted ? "Completed" : "Pending"
          }}</span>
        </div>
      </div>

      <p v-if="errorMessage" class="text-red-500 mt-4">{{ errorMessage }}</p>

      <router-link :to="'/dashboard'">
        <button
          class="mt-6 w-full py-2 bg-pink-500 hover:bg-pink-600 text-white font-semibold rounded-lg shadow-md"
        >
          Back to Dashboard
        </button>
      </router-link>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  props: ["id"],
  data() {
    return {
      todoItem: null,
      errorMessage: null,
    };
  },
  created() {
    this.fetchTodoItem();
  },
  watch: {
    "$route.params.id": "fetchTodoItem",
  },
  methods: {
    async fetchTodoItem() {
      const todoId = this.$route.params.id;
      console.log("Fetching item for ID:", todoId);

      if (!todoId) {
        this.errorMessage = "Invalid Todo ID";
        return;
      }

      try {
        const response = await axios.get(
          `http://localhost:5068/api/todoitem/${todoId}`
        );
        this.todoItem = response.data;
      } catch (error) {
        this.errorMessage = "Error fetching todo item";
        console.error("Error fetching data:", error);
      }
    },
    formatDate(dateString) {
      return dateString ? new Date(dateString).toLocaleString() : "N/A";
    },
  },
};
</script>
