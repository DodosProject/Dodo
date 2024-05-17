import { defineStore } from 'pinia'
import { reactive } from 'vue'
import { useAuthStore } from '@/stores/AuthStore'
import type { User } from '@/core/types'

export const useUserStore = defineStore('userStore', () => {
  const users = reactive<User[]>([])
  const authStore = useAuthStore()
  const user = reactive<User>({
    userId: 0,
    userName: '',
    email: '',
    password: '',
    registrationDate: new Date(),
    penaltyFee: 0,
    role: ''
  })

  async function fetchUsers() {
    if (!authStore.token || !authStore.isAuthenticated || authStore.role != 'admin') {
      console.error('No autorizado: Token no disponible')
      return
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/user`, {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })

      if (!response.ok) {
        throw new Error('Error al obtener usuarios')
      }

      const data = await response.json()
      users.splice(0, users.length, ...data)
    } catch (error) {
      console.error('Error fetching users:', error)
    }
  }

  async function fetchUser(userId: number) {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/user/${userId}`, {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })

      if (!response.ok) {
        throw new Error('Error al obtener el usuario')
      }

      const data = await response.json()
      Object.assign(user, data)
    } catch (error) {
      console.error('Error fetching user:', error)
    }
  }

  const fetchUpdateUser = async (userId: number, name: string, password: string) => {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/User/${userId}`, {
        method: 'PUT',
        headers: {
          Authorization: `Bearer ${authStore.token}`,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, password })
      })

      if (!response.ok) {
        throw new Error('Error en la actualización de usuario.')
      }

      const data = await response.json()
      Object.assign(user, data)
    } catch (error) {
      console.error('Error al actualizar usuario:', error)
    }
  }

  const fetchPayPenaltyFee = async (userId: number): Promise<boolean> => {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return false
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/User/${userId}/paypenaltyfee`, {
        method: 'PUT',
        headers: {
          Authorization: `Bearer ${authStore.token}`,
          'Content-Type': 'application/json'
        }
      })

      if (!response.ok) {
        throw new Error('Error en la actualización de usuario.')
      }

      const data = await response.json()
      Object.assign(user, data)
      return true
    } catch (error) {
      console.error('Error al actualizar usuario:', error)
      return false
    }
  }

  const fetchDeleteUser = async (userId: number): Promise<boolean> => {
    if (!authStore.token || !authStore.isAuthenticated) {
      console.error('No autorizado: Token no disponible')
      return false
    }

    try {
      const response = await fetch(`${authStore.baseUrl}/User/${userId}`, {
        method: 'DELETE',
        headers: {
          Authorization: `Bearer ${authStore.token}`,
          'Content-Type': 'application/json'
        }
      })

      if (!response.ok) {
        throw new Error('Error en la eliminación del usuario.')
      }

      return true
    } catch (error) {
      console.error('Error al eliminar usuario:', error)
      return false
    }
  }

  function clearUsers() {
    users.splice(0, users.length)
    Object.assign(user, {
      userId: 0,
      userName: '',
      email: '',
      password: '',
      registrationDate: new Date(),
      penaltyFee: 0,
      role: ''
    })
  }

  return {
    users,
    user,
    fetchUsers,
    fetchUser,
    fetchUpdateUser,
    fetchPayPenaltyFee,
    fetchDeleteUser,
    clearUsers
  }
})