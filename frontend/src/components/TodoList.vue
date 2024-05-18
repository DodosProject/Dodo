<!-- <template>
  <div class="todo-list">
    <AddTodo />
    <div v-for="todo in todos" :key="todo.taskId">
      <TodoItem :todo="todo.title" @update="fetchTodos" @remove="fetchTodos" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useToDoStore } from '@/stores/todoStore'
import AddTodo from './AddTodo.vue'
import TodoItem from './TodoItem.vue'


const store = useToDoStore()
const todos = store.todos

const fetchTodos = () => {
  store.fetchTodos()
}

onMounted(fetchTodos)
</script>

<style lang="scss" scoped>
.todo-list {
  padding: 20px;
  background: #f4f4f9;
  border-radius: 8px;
}
</style> -->
