<template>
  <div class="add-todo">
    <input v-model="text" @keyup.enter="addTodo" placeholder="Add a new todo" />
    <button @click="addTodo">Add</button>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useToDoStore } from '@/stores/todoStore'

const text = ref('')
const store = useToDoStore()

const addTodo = () => {
  if (text.value.trim()) {
    store.addTodo({
      id: Date.now(),
      text: text.value.trim(),
      completed: false
    })
    text.value = ''
  }
}
</script>

<style lang="scss" scoped>
.add-todo {
  display: flex;
  margin-bottom: 20px;
}
.add-todo input {
  flex: 1;
  padding: 5px;
  margin-right: 10px;
}
.add-todo button {
  padding: 5px 10px;
}
</style>
@/stores/TodoStore@/stores/TodoStore
