<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useToDoStore } from '@/stores/todoStore'
import type { DoTask } from '@/core/types'
import TodoItem from './TodoItem.vue'

const todoStore = useToDoStore()

const emit = defineEmits(['returnList'])

const props = defineProps<{
  list: List
}>()

const listItem = ref<DoTask>({
  taskId: 0,
  title: '',
  description: '',
  creationDate: 0,
  completed: false,
  priority: 0
})

onMounted(async () => {
  // data = cogiendo los libros prestados segun usuario
  const data = await todoStore.fetchTodos()
  if (data != null) {
    listItem.value = data
    console.log('holiiii')
  }
})

const returnList = (listId: number) => {
  emit('returnList', listId)
}
</script>

<template>
  <v-list-item>
    <v-list-item-content v-for="todo in todos" :key="todo.id">
      <TodoItem :todo="todo" @update="fetchTodos" @remove="fetchTodos" />
    </v-list-item-content>

    <div class="divider">
      <v-divider></v-divider>
    </div>
  </v-list-item>
</template>

<style scoped>
.divider {
  padding-top: 15px;
  padding-bottom: 10px;
}
</style>
