<template>
  <div class="home">
    <h1>To Do List</h1>
    <TodoList />
    <h2>Add a new task</h2>
    <AddTodo />
  </div>
</template>

<script setup lang="ts">
import TodoList from "@/components/TodoList.vue";
import AddTodo from "@/components/AddTodo.vue";
</script>

<style lang="scss" scoped>
.home {
  padding: 20px;
  display: flex;
  justify-content: center;
}
</style>
