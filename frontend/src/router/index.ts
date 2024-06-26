import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/stores/AuthStore'
import LoginView from '../views/LoginView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/list',
      name: 'list',
      meta: { requiresAuth: true },
      component: () => import('@/views/TodoListView.vue')
    },
    {
      path: '/maketask',
      name: 'maketask',
      meta: { requiresAuth: true },
      component: () => import('@/views/MakeTodoView.vue')
    }
  ]
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  if (to.path === '/login') {
    next()
    return
  }
  if (!authStore.token) {
    next('/login')
    return
  }

  if (authStore.isTokenExpired()) {
    authStore.logout()
    next('/login')
    return
  }

  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next('/login')
  } else {
    next()
  }
})

export default router
