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

  const fetchDeleteTodo = async (taskId: number): Promise<boolean> => {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return false
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/DoTask/${taskId}`, {
        method: 'DELETE',
        headers: {
          Authorization: `Bearer ${authStore.token}`,
          'Content-Type': 'application/json'
        }
      })

      if (!response.ok) {
        throw new Error('Error en la eliminación de la tarea.')
      }

      await fetchTodos(authStore.userIdLoged)
      return true
    } catch (error) {
      console.error('Error al eliminar la tarea:', error)
      return false
    }
  }

  const fetchCompleteTodo = async (taskId: number): Promise<boolean> => {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return false
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/DoTask/${taskId}/complete`, {
        method: 'PUT',
        headers: {
          Authorization: `Bearer ${authStore.token}`,
          'Content-Type': 'application/json'
        }
      })

      if (!response.ok) {
        throw new Error('Error al completar la tarea.')
      }

      await fetchTodos(authStore.userIdLoged)
      return true
    } catch (error) {
      console.error('Error al completar la tarea:', error)
      return false
    }
  }

  const fetchAddTodo = async (task: DoTask) => {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/DoTask`, {
        method: 'POST',
        headers: {
          Authorization: `Bearer ${authStore.token}`,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(task)
      })

      if (!response.ok) {
        throw new Error('Error en la solicitud de creación de tarea')
      }

      await fetchTodos(authStore.userIdLoged)
    } catch (error) {
      console.error('Error al crear tarea:', error)
    }
  }

  return {
    todos,
    fetchTodos,
    fetchDeleteTodo,
    fetchCompleteTodo,
    fetchAddTodo
  }
})
