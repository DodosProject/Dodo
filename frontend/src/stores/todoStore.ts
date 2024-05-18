import type { DoTask } from '@/core/types'
import { defineStore } from 'pinia'
import { reactive, ref } from 'vue'

export const useToDoStore = defineStore('TodoStore', () => {
 
    const todos = reactive<DoTask[]>([
      {
        taskId: 1,
        title: 'test',
        description: 'test description',
        creationDate: new Date(),
        completed: false,
        priority: 1,
        userId: 1
      },
      {
        taskId: 2,
        title: 'test2',
        description: 'test description2',
        creationDate: new Date(),
        completed: false,
        priority: 2,
        userId: 1
      },
      {
        taskId: 3,
        title: 'test3',
        description: 'test description3',
        creationDate: new Date(),
        completed: false,
        priority: 3,
        userId: 2
      }
    ])

    async function fetchTodos() {
      // Simulate API call
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
    async function fetchDelete(id: number) {

    }
    return {
      todos,
      fetchTodos,
      fetchDelete
      
    }
  })