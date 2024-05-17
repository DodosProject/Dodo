import type { DoTask } from '@/core/types'
import { defineStore } from 'pinia'

export const useToDoStore = defineStore('todoStore', {
  state: () => ({
    todos: [] as DoTask[]
  }),
  actions: {
    async fetchTodos() {
      // Simulate API call
      this.todos = [
        {
          taskId: 1,
          title: 'test',
          description: 'test description',
          creationDate: 1,
          completed: false,
          priority: 1
        }
      ]
    },
    addTodo(todo: DoTask) {
      this.todos.push(todo)
    },
    toggleTodoCompletion(taskId: number) {
      const todo = this.todos.find((todo) => todo.taskId === taskId)
      if (todo) {
        todo.completed = !todo.completed
      }
    },
    removeTodo(taskId: number) {
      this.todos = this.todos.filter((todo) => todo.taskId !== taskId)
    }
  }
})
