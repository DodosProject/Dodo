<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useToDoStore } from '@/stores/todoStore'
import type { DoTask } from '@/core/types'


const todoStore = useToDoStore()

const listItem = ref<DoTask>({
  taskId: 0,
  title: 'asdfasda',
  description: '',
  creationDate: 0,
  completed: false,
  priority: 0
}) 

const props = defineProps<{
  tasks: {
    taskId: number
    title: string
    description: string
    creationDate: number
    completed: boolean
    priority: number
  }
}>()

const emit = defineEmits(['update', 'remove'])

const store = useToDoStore()


onMounted(async () => {
  const data = await todoStore.fetchTodos()
  if (data != null) {
    listItem.value = data
  }
})

const toggleCompletion = () => {
  store.toggleTodoCompletion(props.tasks.taskId)
  emit('update')
}

const removeTodo = () => {
  store.removeTodo(props.tasks.taskId)
  emit('remove')
}
</script>

<template>
  <v-list-item>
    <div class="todo-item">
      <input type="checkbox" />
      <div><strong>Task: </strong>{{ listItem.title }}</div>
      <div>
          <!-- <span :class="{ completed: tasks.completed }">{{ tasks.title }}</span> -->
      </div>
      <button @click="removeTodo">Remove</button>
    </div>
    <div class="divider">
      <v-divider></v-divider>
    </div>
  </v-list-item>

 <!--  <v-list>
    <TodoItem
      v-for="task in tasks"
      :key="props.tasks.taskId"
      :tasks="props.tasks.taskId"
      @returnList="returnList"
    />
  </v-list> -->


  
</template>

<style scoped>
.divider {
  padding-top: 15px;
  padding-bottom: 10px;
}

.todo-list {
  padding: 20px;
  background: #f4f4f9;
  border-radius: 8px;
}
</style>
