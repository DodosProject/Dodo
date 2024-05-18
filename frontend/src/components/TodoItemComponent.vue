<script setup lang="ts">
import type { DoTask } from '@/core/types'

const props = defineProps<{
  todo: DoTask
}>()

const emit = defineEmits(['todoCompleted', 'todoDeleted'])

const emitComplete = (id: number) => {
  emit('todoCompleted', id)
}

const emitDelete = (id: number) => {
  emit('todoDeleted', id)
}
</script>

<template>
  <v-list-item>
    <v-list-item-content class="task-content">
      <v-list-item-title class="headline">Task: {{ props.todo.title }}</v-list-item-title>
      <v-list-item-subtitle class="subtitle-1"
        >Description: {{ props.todo.description }}</v-list-item-subtitle
      >
      <v-list-item-subtitle class="subtitle-1"
        >Is Completed: {{ props.todo.completed }}</v-list-item-subtitle
      >
    </v-list-item-content>
    <v-list-item-action class="task-content">
      <v-btn class="app-button" v-if="!props.todo.completed" @click="emitComplete(props.todo.doTaskId)">
        Complete
      </v-btn>
      <v-btn class=".delete-button" @click="emitDelete(props.todo.doTaskId)">Delete</v-btn>
    </v-list-item-action>
    <v-divider></v-divider>
  </v-list-item>
</template>