<template>
    <div class="todo-list">
      <AddTodo />
      <div v-for="todo in todos" :key="todo.id">
        <TodoItem
          :todo="todo"
          @update="fetchTodos"
          @remove="fetchTodos"
        />
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { onMounted, ref } from "vue";
  import { useTodoStore } from "@/stores/todoStore"
  import AddTodo from "./AddTodo.vue";
  import TodoItem from "./TodoItem.vue";
  
  const text = ref("");
  const store = useTodoStore();
  const todos = store.todos;
  
  const fetchTodos = () => {
    store.fetchTodos();
  };
  
  onMounted(fetchTodos);
  </script>
  
  <style lang="scss" scoped>
  .todo-list {
    padding: 20px;
    background: #f4f4f9;
    border-radius: 8px;
  }
  </style>
  