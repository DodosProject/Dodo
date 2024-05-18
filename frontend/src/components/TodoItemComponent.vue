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
      <v-btn v-if="!props.todo.completed" @click="emitComplete(props.todo.doTaskId)" class="btn">
        Complete
      </v-btn>
      <v-btn @click="emitDelete(props.todo.doTaskId)" class="delete-btn">Delete</v-btn>
    </v-list-item-action>
    <v-divider></v-divider>
  </v-list-item>
</template>

<style scoped>
.headline {
  font-size: 1rem;
}

.subtitle-1 {
  font-size: 0.75rem;
}

.task-content {
  float: left;
  width: 50%;
  padding: 10px;
}

.btn {
  margin: 0px;
  left: -20%;
  font-size: 0.6rem;
}

@media (max-width: 1280px) {
  .headline {
    font-size: 0.75rem;
  }

  .subtitle-1 {
    font-size: 0.5rem;
  }
}

@media (max-width: 880px) {
  .btn {
    margin: 0px;
    left: -20%;
    font-size: 0.4rem;
  }

  .loan-btn {
    margin: 0px;
    font-size: 0.4rem;
  }
}
</style>
