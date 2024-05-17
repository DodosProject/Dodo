<template>
    <div class="todo-item">
      <input
        type="checkbox"
        v-model="todo.completed"
        @change="toggleCompletion"
      />
      <span :class="{ completed: todo.completed }">{{ todo.text }}</span>
      <button @click="removeTodo">Remove</button>
    </div>
  </template>
  
  <script setup lang="ts">
  import { useTodoStore } from "@/stores/todoStore";
  
  const props = defineProps<{
    todo: {
      id: number;
      text: string;
      completed: boolean;
    };
  }>();
  
  const emit = defineEmits(["update", "remove"]);
  
  const store = useTodoStore();
  
  const toggleCompletion = () => {
    store.toggleTodoCompletion(props.todo.id);
    emit("update");
  };
  
  const removeTodo = () => {
    store.removeTodo(props.todo.id);
    emit("remove");
  };
  </script>
  
  <style lang="scss" scoped>
  .todo-item {
    display: flex;
    align-items: center;
    margin-bottom: 10px;
  }
  .todo-item span.completed {
    text-decoration: line-through;
  }
  .todo-item button {
    margin-left: auto;
  }
  </style>
  