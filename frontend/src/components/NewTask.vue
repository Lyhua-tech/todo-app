<template>
  <div class="max-w-[600px] w-full">
    <button
      @click="isOpen"
      class="bg-stone-100 border w-full h-14 rounded text-3xl font-bold"
    >
      +
    </button>

    <div
      v-if="open"
      class="fixed inset-0 bg-black opacity-80 z-50"
      @click="isOpen"
    ></div>

    <section
      :class="{
        'right-0': open,
        'right-[-400px]': !open,
      }"
      class="fixed top-0 w-[300px] h-screen bg-stone-100 text-stone-600 border-l border-stone-400 z-50 p-4 transition-all duration-500"
    >
      <h1 class="text-xl font-bold mb-4">Daily Plan</h1>

      <form @submit.prevent="addTodo" class="flex flex-col gap-4">

        <div class="border px-2 py-2 rounded border-stone-400">
          <label class="block font-bold">Title:</label>
          <input
            type="text"
            v-model="title"
            class="w-full focus:outline-none bg-transparent"
            required
          />
        </div>

        <div class="border px-2 py-2 rounded border-stone-400">
          <label class="block font-bold">Content:</label>
          <textarea
            v-model="content"
            class="w-full focus:outline-none bg-transparent resize-none"
            rows="4"
            required
          ></textarea>
        </div>

        <button
          type="submit"
          class="bg-pink-600 text-white font-bold py-2 rounded hover:bg-pink-700"
        >
          Add
        </button>
      </form>
    </section>
  </div>
</template>

<script>
import axios from "axios";
import { ref } from "vue";

export default {
  setup() {
    const open = ref(false);
    const title = ref("");
    const content = ref("");

    const isOpen = () => {
      open.value = !open.value;
    };

    const addTodo = async () => {
      if (!title.value || !content.value) return;
      await axios.post("http://localhost:5068/api/todoitem", {
        title: title.value,
        content: content.value,
        isComplete: false,
      });

      title.value = "";
      content.value = "";
      open.value = false;

      this.$emit("refresh");
    };

    return { open, isOpen, title, content, addTodo };
  },
};
</script>
