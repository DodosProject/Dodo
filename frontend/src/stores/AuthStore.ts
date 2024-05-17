import { defineStore } from 'pinia'
import { useRouter } from 'vue-router'
import { ref } from 'vue'
// import { useToDoStore } from '@/stores/todoStore'
import { useUserStore } from '@/stores/UserStore'

export const useAuthStore = defineStore('authStore', () => {
  const baseUrl = ref('http://localhost:7790') //PRO
  //const baseUrl = ref('http://localhost:7791') //PRE
  //const baseUrl = 'https://bookybookapi-pre.azurewebsites.net'

  const isAuthenticated = ref(false)
  //const role = ref<string | null>(localStorage.getItem('role') ?? null)
  const token = ref<string | null>(localStorage.getItem('token') ?? null)
  const decodedToken = ref<DecodedToken | null>()
  const router = useRouter()
  const userIdLoged = ref<number>(0)

  function setToken(newToken: string) {
    token.value = newToken
    localStorage.setItem('token', newToken)
    decodedToken.value = decodeJwt(newToken)
    if (decodedToken.value) {
      isAuthenticated.value = true
      //role.value = decodedToken.value.role
      userIdLoged.value = parseInt(decodedToken.value.nameid, 10)
      //localStorage.setItem('role', decodedToken.value.role)
    }
  }

  function decodeJwt(token: string): DecodedToken | null {
    if (!token) {
      return null
    }

    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    try {
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map((c) => {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
          })
          .join('')
      )

      return JSON.parse(jsonPayload) as DecodedToken
    } catch (error) {
      console.error('Error al decodificar el token:', error)
      return null
    }
  }

  const login = async (email: string, password: string) => {
    try {
      const response = await fetch(`${baseUrl.value}/auth/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
      })

      if (!response.ok) {
        throw new Error('Error en la solicitud de inicio de sesión')
      }

      const data = await response.text()
      setToken(data)
    } catch (error) {
      console.error('Error al iniciar sesión:', error)
    }
  }

  const register = async (username: string, email: string, password: string) => {
    try {
      const response = await fetch(`${baseUrl.value}/auth/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, email, password })
      })

      if (!response.ok) {
        throw new Error('Error en la solicitud de registro')
      }

      await login(email, password)
    } catch (error) {
      console.error('Error al iniciar sesión:', error)
    }
  }

  function logout() {
    localStorage.removeItem('token')
    //localStorage.removeItem('role')

    token.value = null
    //role.value = null
    isAuthenticated.value = false
    userIdLoged.value = 0
    decodedToken.value = null
    const userStore = useUserStore()
    userStore.clearUsers()

    router.push('/login')
  }

  function restoreSession() {
    const storedToken = localStorage.getItem('token')
    if (storedToken) {
      decodedToken.value = decodeJwt(storedToken)
      if (!decodedToken.value) {
        logout()
      } else {
        setToken(storedToken)
        isAuthenticated.value = true
        //role.value = decodedToken.value.role
        userIdLoged.value = parseInt(decodedToken.value.nameid, 10)
      }
    }
  }

  function isTokenExpired(): boolean {
    if (!token.value) return true

    try {
      const decoded = decodeJwt(token.value)
      if (!decoded) return true

      const currentTime = Math.floor(Date.now() / 1000)
      return decoded.exp < currentTime
    } catch (error) {
      console.error('Error al verificar la expiración del token:', error)
      return true
    }
  }

  restoreSession()

  return {
    baseUrl,
    isAuthenticated,
    //role,
    token,
    userIdLoged,
    login,
    register,
    logout,
    restoreSession,
    isTokenExpired
  }
})

interface DecodedToken {
  exp: number
  nameid: string
  //role: string
}
