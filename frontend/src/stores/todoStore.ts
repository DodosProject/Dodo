import { defineStore } from 'pinia'

interface Todo {
  id: number
  text: string
  completed: boolean
}

export const useTodoStore = defineStore('todoStore', {
  state: () => ({
    todos: [] as Todo[]
  }),
  actions: {
    async fetchTodos() {
      // Simulate API call
      this.todos = [
        { id: 1, text: 'Learn Vue 3', completed: false },
        { id: 2, text: 'Learn Pinia', completed: false }
      ]
    },
    addTodo(todo: Todo) {
      this.todos.push(todo)
    },
    toggleTodoCompletion(id: number) {
      const todo = this.todos.find((todo) => todo.id === id)
      if (todo) {
        todo.completed = !todo.completed
      }
    },
    removeTodo(id: number) {
      this.todos = this.todos.filter((todo) => todo.id !== id)
    }
  }
})
