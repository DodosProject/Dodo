<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { useToDoStore } from '@/stores/todoStore'
import { useRouter } from 'vue-router'
import type { DoTask } from '@/core/types'
import TodoItemComponent from './TodoItemComponent.vue'

const todoStore = useToDoStore()

const showDetails = ref(false)
const todos = todoStore.todos
const todoDetail = reactive<DoTask>({
  taskId: 0,
  title: '',
  description: '',
  creationDate: 0,
  completed: false,
  priority: 0
})

const handleTaskSelected = (todos: DoTask) => {
  Object.assign(todoDetail, todos)
  showDetails.value = true
}

onMounted(async () => {
  const data = await todoStore.fetchTodos()
  if (data != null) {
    todos.values = data
  }
})
</script>

<template>
  <v-container>
    <v-row>
      <v-col cols="8">
        <h1 class="display-2">To Do List</h1>
        <v-card>
          <v-card-text>
            <v-list>
              <test-todo-list-comp
                v-for="todo in todos"
                :key="todo.taskId"
                :book="todo"
                @taskSelected="handleTaskSelected"
              />
            </v-list>
          </v-card-text>
        </v-card>
      </v-col>
      <!--   <v-col cols="4">
        <Book-Details-Component v-if="showDetails" :book="bookDetails" />
      </v-col> -->
    </v-row>
  </v-container>
</template>

<style scoped>
.display-2 {
  font-size: 2rem;
}

.v-container {
  padding-top: 0;
}

@media (max-width: 700px) {
  .v-container {
    padding-top: 100px;
  }
}
</style>
