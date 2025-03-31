<template>
  <div class="max-w-[600px] w-full">
    <Button
      @click="isOpen"
      class="bg-stone-100 border w-full h-14 rounded text-3xl font-bold"
      >+</Button
    >
    <div :class="this.open ? 'block bg-black' : 'hidden'" c></div>
    <section
      :class="{
        'z-[100] w-0 right-0 duration-500': this.open == true,
        'z-[100] duration-500 right-[-400px]': this.open == false,
      }"
      class="absolute bg-stone-100 w-[300px] h-screen overflow-hidden top-0 text-stone-600 border-l z-50 text-lg border-stone-400"
    >
      <h1 class="text-xl font-bold m-3">Daily Plan</h1>
      <form @submit.prevent="addTodo">
        <!-- <div class="m-3 border px-1 py-2 rounded border-stone-400 font-sans">
          <label>Title : </label>
          <input type="text" class="focus:outline-hidden" />
        </div> -->
        <div class="m-3 border px-1 py-2 rounded border-stone-400 font-sans">
          <label>Title : </label>
          <input type="text" class="focus:outline-hidden" v-model="title" />
        </div>
        <button type="submit">Add</button>
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
    const isOpen = () => {
      open.value = !open.value;
    };

    return { open, isOpen };
  },
  data() {
    return { title: "" };
  },
  methods: {
    async addTodo() {
      if (!this.title) return;
      const res = await axios.post("http://localhost:5068/api/todoitem", {
        title: this.title,
        isComplete: false,
      });
      this.title = "";
      this.$emit("refresh");
    },
  },
};
</script>
