import type { DoTask } from '@/core/types'
import { defineStore } from 'pinia'
import { reactive, ref } from 'vue'
import { useAuthStore } from './AuthStore'

export const useToDoStore = defineStore('TodoStore', () => {
  const authStore = useAuthStore()
  const todos = reactive<DoTask[]>([])

  async function fetchTodos(userId: number): Promise<boolean> {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return false
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/User/${userId}/tasks`, {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })

      if (!response.ok) {
        throw new Error('Error al obtener las tareas del usuario ' + userId)
      }

      const data = await response.json()
      todos.splice(0, todos.length, ...data)
      return true
    } catch (error) {
      console.error('Error fetching user tasks:', error)
      return false
    }
  }

  // addTodo(todo: DoTask) {
  //   this.todos.push(todo)
  // }
  // toggleTodoCompletion(taskId: number) {
  //   const todo = this.todos.find((todo) => todo.taskId === taskId)
  //   if (todo) {
  //     todo.completed = !todo.completed
  //   }
  // }
  // removeTodo(taskId: number) {
  //   this.todos = this.todos.filter((todo) => todo.taskId !== taskId)
  // }
  async function fetchDelete(id: number) {}
  return {
    todos,
    fetchTodos,
    fetchDelete
  }
})
