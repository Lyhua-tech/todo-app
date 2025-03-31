<template>
  <ul class="max-w-[600px] w-full mt-10">
    <li
      v-for="(todo, index) in todos"
      :key="todo.id"
      class="flex items-center justify-between w-full border-1 rounded my-1.5 px-3 text-xl font-semibold h-[56px]"
    >
      <div class="flex items-center gap-3">
        <input
          type="checkbox"
          v-model="todo.isCompleted"
          @change="updateTodo(todo)"
          class="size-4 accent-emerald-600"
        />
        <span
          v-if="!todo.isEditing"
          :class="{
            'text-emerald-600': todo.isCompleted,
            'text-amber-500': !todo.isCompleted,
          }"
        >
          {{ index + 1 }}. {{ todo.title }}
        </span>
        <input
          type="text"
          v-else
          v-model="todo.title"
          @change="editTitle(todo)"
          class="px-1 focus:outline-hidden rounded h-[45px] ml-2.5 field-sizing-fixed w-[400px]"
          :class="{
            'text-emerald-600': todo.isCompleted,
            'text-amber-500': !todo.isCompleted,
            'break-words': todo.title.length > 50,
            truncate: todo.title.length <= 50,
          }"
        />
      </div>
      <div>
        <button
          @click="deleteTodo(todo.id)"
          class="group py-1 px-2 bg-zinc-100 hover:bg-emerald-600 rounded transition"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke-width="1.5"
            stroke="currentColor"
            class="size-6 text-emerald-600 group-hover:text-zinc-100 transition"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="m14.74 9-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 0 1-2.244 2.077H8.084a2.25 2.25 0 0 1-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 0 0-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 0 1 3.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 0 0-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 0 0-7.5 0"
            />
          </svg>
        </button>
        <button
          v-if="!todo.isEditing"
          @click="toggleEdit(todo)"
          class="group py-1 px-2 bg-zinc-100 hover:bg-emerald-600 rounded transition"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke-width="1.5"
            stroke="currentColor"
            class="size-6 text-emerald-600 group-hover:text-zinc-100 transition"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 16.07a4.5 4.5 0 0 1-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 0 1 1.13-1.897l8.932-8.931Zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0 1 15.75 21H5.25A2.25 2.25 0 0 1 3 18.75V8.25A2.25 2.25 0 0 1 5.25 6H10"
            />
          </svg>
        </button>
        <button
          v-if="todo.isEditing"
          @click="toggleEdit(todo)"
          class="group py-1 px-2 bg-zinc-100 hover:bg-emerald-600 rounded transition"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke-width="1.5"
            stroke="currentColor"
            class="size-6 text-emerald-600 group-hover:text-zinc-100 transition"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M9 12.75 11.25 15 15 9.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
            />
          </svg>
        </button>
      </div>
    </li>
  </ul>
</template>

<script>
import axios from "axios";

export default {
  props: ["todos"],
  methods: {
    async updateTodo(todo) {
      await axios.put(`http://localhost:5068/api/todoitem/${todo.id}`, todo);
    },
    async deleteTodo(id) {
      await axios.delete(`http://localhost:5068/api/todoitem/${id}`);
      this.$emit("refresh");
    },
    toggleEdit(todo) {
      if (!todo.hasOwnProperty("isEditing")) {
        todo["isEditing"] = false;
      }
      todo.isEditing = !todo.isEditing;
    },
    async editTitle(todo) {
      await axios.put(`http://localhost:5068/api/todoitem/${todo.id}`, todo);

      todo["isEditing"] = false;
    },
  },
};
</script>
