<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '@/stores/AuthStore'

const authStore = useAuthStore()
authStore.isAuthenticated
</script>

<template>
  <img alt="Vue logo" class="logo" src="@/assets/dodo.svg" width="125" height="125" />
  <div class="wrapper">
    <nav>
      <RouterLink class="nav-btn" v-if="!authStore.isAuthenticated" to="/login">Login</RouterLink>
      <a class="nav-btn" @click="authStore.logout()" v-if="authStore.isAuthenticated">Logout</a>
      <RouterLink class="nav-btn" v-if="authStore.isAuthenticated" to="/list">ToDo List</RouterLink>
      <RouterLink class="nav-btn" v-if="authStore.isAuthenticated" to="/maketask"
        >Make task</RouterLink
      >
    </nav>
  </div>
</template>

<style scoped>
.nav-btn {
  text-decoration: none;
  cursor: pointer;
  color: rgb(78, 78, 78);
  padding: 5px;
}
</style>
