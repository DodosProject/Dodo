<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import type { DoTask } from '@/core/types'
import { useToDoStore } from '@/stores/todoStore'
import { useAuthStore } from '@/stores/AuthStore'
import TodoItemComponent from './TodoItemComponent.vue'

const todoStore = useToDoStore()
const authStore = useAuthStore()
const todos = computed(() => todoStore.todos)


const handleTaskCompleted = (id: number) => {
 todoStore.fetchCompleteTodo(id)
}

const handledTaskDeleted = (id: number) => {
  todoStore.fetchDeleteTodo(id)
}

onMounted(async () => {
  await todoStore.fetchTodos(authStore.userIdLoged)
})
</script>

<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1 class="display-2">To Do List</h1>
        <v-card>
          <v-card-text>
            <v-list>
              <todo-item-component
                v-for="todo in todos"
                :key="todo.doTaskId"
                :todo="todo"
                @todoCompleted="handleTaskCompleted"
                @todoDeleted="handledTaskDeleted"
              />
            </v-list>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>