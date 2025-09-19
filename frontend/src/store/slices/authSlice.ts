import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import api from '../../api/axios'

interface UserDto { id: number; email: string; role: string }
interface AuthState {
  token: string | null
  user: UserDto | null
  loading: boolean
  error?: string | null
}

const initialState: AuthState = {
  token: localStorage.getItem('token') && localStorage.getItem('token') !== 'undefined'
    ? localStorage.getItem('token')
    : null,
  user: localStorage.getItem('user') && localStorage.getItem('user') !== 'undefined'
    ? JSON.parse(localStorage.getItem('user')!)
    : null,
  loading: false,
  error: null
}



export const login = createAsyncThunk('auth/login', async (payload: { email: string; password: string }, thunkAPI) => {
  const res = await api.post('/api/auth/login', payload)
  return res.data
})

export const register = createAsyncThunk('auth/register', async (payload: { username: string; email: string; password: string }, thunkAPI) => {
  const res = await api.post('/api/auth/register', payload)
  return res.data
})

const slice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout(state) {
      state.token = null
      state.user = null
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    }
  },
  extraReducers: builder => {
    builder.addCase(login.pending, state => { state.loading = true; state.error = null })
    builder.addCase(login.fulfilled, (state, action) => {
      state.loading = false
      state.token = action.payload.result
      state.user = null
      localStorage.setItem('token', action.payload.result ?? '')
    })

    builder.addCase(login.rejected, (state, action) => { state.loading = false; state.error = action.error.message ?? 'Login failed' })

    builder.addCase(register.pending, state => { state.loading = true; state.error = null })
    builder.addCase(register.fulfilled, (state) => { state.loading = false })
    builder.addCase(register.rejected, (state, action) => { state.loading = false; state.error = action.error.message ?? 'Register failed' })
  }
})

export const { logout } = slice.actions
export default slice.reducer;
