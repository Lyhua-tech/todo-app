<template>
  <form
    @submit.prevent="addTodo"
    class="border-1 rounded flex items-center text-xl pl-2 mt-3"
  >
    <input
      v-model="title"
      placeholder="Tell me your plan..."
      class="focus:outline-hidden"
    />
    <button
      type="submit"
      class="border border-transparent hover:border-1 py-0.5 px-2 m-1 rounded hover:bg-emerald-600 hover:text-white transition ease-in-out"
    >
      add
    </button>
  </form>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return { title: "" };
  },
  methods: {
    async addTodo() {
      if (!this.title) return;
      await axios.post("http://localhost:5068/api/todoitem", {
        title: this.title,
        isComplete: false,
      });
      this.title = "";
      this.$emit("refresh");
    },
  },
};
</script>
