<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="@/assets/DoDoApp.png" width="125" height="125" />

    <div class="wrapper">
      <nav>
        <RouterLink to="/login">Login</RouterLink>
        <RouterLink to="/list">TODO List</RouterLink>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>

</style>
