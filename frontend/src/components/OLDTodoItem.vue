<!-- <template>
  <div class="todo-item">
    <input type="checkbox" v-model="tasks.completed" @change="toggleCompletion" />
    <div>
      <span :class="{ completed: tasks.completed }">{{ tasks.title }}</span>
    </div>

    <button @click="removeTodo">Remove</button>
  </div>
</template>

<script setup lang="ts">
import { useToDoStore } from '@/stores/todoStore'

const props = defineProps<{
  tasks: {
    taskId: number
    title: string
    description: string
    creationDate: number
    completed: boolean
    priority: number
  }
}>()

const emit = defineEmits(['update', 'remove'])

const store = useToDoStore()

const toggleCompletion = () => {
  store.toggleTodoCompletion(props.tasks.taskId)
  emit('update')
}

const removeTodo = () => {
  store.removeTodo(props.tasks.taskId)
  emit('remove')
}
</script>

<style lang="scss" scoped>
.todo-item {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}
.todo-item span.completed {
  text-decoration: line-through;
}
.todo-item button {
  margin-left: auto;
}
</style>
-->