<template>
  <div class="w-full m-2">
    <h1
      v-if="period === 1"
      class="text-xl font-bold text-start ml-3 underline"
      :class="{
        hidden: todoHistory.length === 0,
      }"
    >
      Recently
    </h1>
    <h1
      v-else
      class="text-xl font-bold text-start ml-3 underline"
      :class="{
        hidden: todoHistory.length === 0,
      }"
    >
      Last Week
    </h1>
    <ul class="my-3">
      <li v-for="todo in todoHistory" :key="todo.id" class="text-md">
        <p
          class="flex justify-between py-3 text-stone-500 my-1 px-3 items-center border-stone-400"
        >
          <span> {{ todo.title }} </span>
          <span class="font-semibold text-[14px] opacity-75">
            {{ formatDate(todo.completeAt) }}
          </span>
        </p>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      todoHistory: [],
    };
  },
  props: {
    period: Number,
  },
  methods: {
    formatDate(dateString) {
      const date = new Date(dateString);
      const today = new Date();
      const time = Math.floor((today - date) / (1000 * 60 * 60 * 24));

      if (time == 0) {
        const hours = this.formatHour(dateString);
        console.log(hours);
        if (hours == 0) {
          return "now";
        } else if (hours == 1) {
          return "1 hour ago";
        } else {
          return `${hours} hours ago`;
        }
      } else if (time == 1) {
        return "1 day ago";
      }
      return `${time} days ago`;
    },

    formatHour(dataString) {
      const date = new Date(dataString);
      const todayHour = new Date();
      return Math.floor((todayHour - date) / (1000 * 60 * 60));
    },

    async fetchHistory(period) {
      try {
        const res = await axios.get(
          `http://localhost:5068/api/todoitem/complete/${period}`
        );
        this.todoHistory = res.data;
      } catch (error) {
        console.error("Failed to fetch history:", error);
      }
    },
  },
  mounted() {
    this.fetchHistory(this.period);
  },
};
</script>
