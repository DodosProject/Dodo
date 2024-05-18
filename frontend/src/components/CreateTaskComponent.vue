<script setup lang="ts">
import { ref, computed } from 'vue'
import { useToDoStore } from '@/stores/todoStore'
import type { DoTask } from '@/core/types'
import router from '@/router';

const todo = ref<DoTask>({
  doTaskId: 0,
  title: '',
  description: '',
  creationDate: new Date(),
  completed: false,
  priority: 0,
  userId: 0
})

const titleRules = [
  (v: string) => !!v || 'El título es requerido',
  (v: string) => (v && v.length <= 100) || 'El título debe tener menos de 100 caracteres'
]
const descriptionRules = [
  (v: string) => !!v || 'La descripción es requerido',
  (v: string) => (v && v.length <= 100) || 'La descripción debe tener menos de 100 caracteres'
]

const valid = computed(() => {
  return (
    todo.value.title !== '' &&
    todo.value.description !== '' &&
    (todo.value.priority === null || (todo.value.priority >= 0 && todo.value.priority <= 10))
  )
})

const todoStore = useToDoStore()

const submitTask = () => {
  if (valid.value) {
    todoStore.fetchAddTodo(todo.value)
    todo.value = {
      doTaskId: 0,
      title: '',
      description: '',
      creationDate: new Date(),
      completed: false,
      priority: 0,
      userId: 0
    }
  }
  router.push('/list')
}
</script>

<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12">
        <v-card>
          <v-card-title>Make a new Task</v-card-title>
          <v-card-text>
            <v-form ref="form" v-model="valid" lazy-validation>
              <v-text-field
                v-model="todo.title"
                :rules="titleRules"
                label="Title"
                required
              ></v-text-field>
              <v-text-field
                v-model="todo.description"
                :rules="descriptionRules"
                label="Description"
                required
              ></v-text-field>
              <v-slider
                v-model="todo.priority"
                :max="10"
                step="1"
                ticks="always"
                tick-size="9"
                label="Priority"
              ></v-slider>
              <span class="score">{{ todo.priority }}</span>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-btn :disabled="!valid" class=".app-button" @click="submitTask"> Make Task </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.score {
  margin-left: 20px;
  font-size: 1.5rem;
}

.v-card-title {
  font-size: 1.5rem;
}

.v-btn {
  margin: 10px;
  font-size: 1rem;
}
</style>
