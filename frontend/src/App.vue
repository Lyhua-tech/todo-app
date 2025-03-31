<template>
  <div
    class="text-emerald-600 max-w-[1600px] mx-auto flex flex-col items-center mt-20"
  >
    <h1 class="text-xl font-bold">ToDo Application</h1>
    <AddTask @refresh="fetchTodos" />
    <TodoList :todos="todos" @refresh="fetchTodos" />
    <NewTask @refresh="fetchTodos" />
    <h1 class="text-2xl font-bold text-start text-emerald-600 ml-3 mb-3 my-2">
      History
    </h1>
  </div>
  <section class="max-w-[600px] mx-auto w-full">
    <div
      class="border-2 border-stone-300 rounded-lg text-stone-500 flex flex-col items-center justify-center"
    >
      <TaskHistory :period="period[0]" />
      <div class="bg-stone-300 max-w-[580px] h-[2px] rounded w-full"></div>
      <TaskHistory :period="period[1]" />
    </div>
  </section>
</template>

<script>
import axios from "axios";

import AddTask from "./components/AddTask.vue";
import TodoList from "./components/TodoList.vue";
import TaskHistory from "./components/TaskHistory.vue";
import NewTask from "./components/NewTask.vue";
export default {
  components: { AddTask, TodoList, TaskHistory, NewTask },
  data() {
    return { todos: [], period: [1, 7] };
  },
  methods: {
    async fetchTodos() {
      try {
        const res = await axios.get("http://localhost:5068/api/todoitem");
        this.todos = res.data;
      } catch (error) {
        console.log("error occur", error);
      }
    },
  },
  mounted() {
    this.fetchTodos();
  },
};
</script>
