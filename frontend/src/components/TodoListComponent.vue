<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import type { DoTask } from '@/core/types'
import { useToDoStore } from '@/stores/todoStore'
import TodoItemComponent from './TodoItemComponent.vue'

const todoStore = useToDoStore()
const todos = computed(() => todoStore.todos)
const showDetails = ref(false)

// const todoDetail = reactive<DoTask>({
//   taskId: 0,
//   title: '',
//   description: '',
//   creationDate: new Date(),
//   completed: false,
//   priority: 0,
//   userId: 0
// })

const handleTaskCompleted = (id: number) => {
  //todoStore.fetchComplete(id)
  let foundTask = todoStore.todos.find(td => td.taskId === id)
  if (foundTask){
    foundTask.completed = true
  }

}

const handledTaskDeleted = (id: number) => {
  todoStore.fetchDelete(id)
}

onMounted(async () => {
  // const data = await todoStore.fetchTodos()
  // if (data != null) {
  //   todos.values = data
  // }
  
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
                :key="todo.taskId"
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
@/stores/TodoStore@/stores/todoStore@/stores/todoStore@/stores/TodoStore@/stores/todoStore