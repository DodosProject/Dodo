<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/AuthStore'

const router = useRouter()

const authStore = useAuthStore()

const email = ref('')
const password = ref('')

const snackbar = ref(false)
const snackbarMessage = ref('')

const login = async () => {
  await authStore.login(email.value, password.value)
  if (authStore.isAuthenticated) {
    console.log('Sesión iniciada con éxito')
    router.push('/')
  } else {
    console.error('Error al iniciar sesión')
    snackbarMessage.value = 'Error al iniciar sesión'
    snackbar.value = true
  }
}
</script>

<template>
  <v-container class="login-container">
    <v-card class="login-card">
      <v-card-title class="title">Log In</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="login">
          <v-text-field v-model="email" label="Email" outlined></v-text-field>
          <v-text-field
            v-model="password"
            label="Password"
            type="password"
            outlined
          ></v-text-field>
          <v-btn type="submit" color="primary" block>Log In</v-btn>
        </v-form>
      </v-card-text>
    </v-card> 
  </v-container>
  <v-snackbar class="error" v-model="snackbar" :timeout="3000" bottom right>
    {{ snackbarMessage }}
    <br>
    <v-btn color="red" @click="snackbar = false">Ok</v-btn>
  </v-snackbar>
</template>