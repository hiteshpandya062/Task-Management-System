import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit'
import api from '../../api/axios'

export type Task = {
  id?: number
  title: string
  description: string
  status: 'ToDo' | 'InProgress' | 'Done'
  assigneeName?: string
  assigneeId?: number
  priority?: number
  creatorName?: string
  createdAt?: string
  updatedAt?: string
}

const mapStatus = (status: number): Task['status'] =>
  status === 1 ? 'ToDo' : status === 2 ? 'InProgress' : 'Done'

export const fetchTasks = createAsyncThunk<Task[]>('tasks/fetch', async () => {
  const res = await api.get('/api/tasks')
  return (res.data.result ?? []).map((t: any) => ({
    ...t,
    status: mapStatus(t.status)
  }))
})

export const createTask = createAsyncThunk<Task, Partial<Task>>(
  'tasks/create',
  async (payload) => {
    const res = await api.post('/api/tasks', payload)
    return { ...res.data.result, status: mapStatus(res.data.result.status), priority: null }
  }
)

export const updateTask = createAsyncThunk<Task, { id: number; payload: Partial<Task> }>(
  'tasks/update',
  async ({ id, payload }) => {
    const res = await api.put(`/api/tasks/${id}`, payload)
    return { ...res.data.result, status: mapStatus(res.data.result.status) }
  }
)

export const deleteTask = createAsyncThunk<number, number>('tasks/delete', async (id) => {
  await api.delete(`/api/tasks/${id}`)
  return id
})

type TasksState = { items: Task[]; loading: boolean; error: string | null }
const initialState: TasksState = { items: [], loading: false, error: null }

const tasksSlice = createSlice({
  name: 'tasks',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchTasks.pending, (state) => { state.loading = true })
    builder.addCase(fetchTasks.fulfilled, (state, action: PayloadAction<Task[]>) => {
      state.loading = false
      state.items = action.payload
    })
    builder.addCase(fetchTasks.rejected, (state, action) => {
      state.loading = false
      state.error = action.error.message ?? 'Failed to fetch tasks'
    })

    builder.addCase(createTask.fulfilled, (state, action: PayloadAction<Task>) => {
      state.items.push(action.payload)
    })

    builder.addCase(updateTask.fulfilled, (state, action: PayloadAction<Task>) => {
      state.items = state.items.map((t) => (t.id === action.payload.id ? action.payload : t))
    })

    builder.addCase(deleteTask.fulfilled, (state, action: PayloadAction<number>) => {
      state.items = state.items.filter((t) => t.id !== action.payload)
    })
  }
})

export default tasksSlice.reducer
