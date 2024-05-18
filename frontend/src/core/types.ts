export interface User {
  userId: number
  name: string
  email: string
  password: string
  registrationDate: Date
}
export interface DoTask {
  taskId: number
  title: string
  description: string
  creationDate: Date
  completed: Boolean
  priority: number
  userId: number
}
