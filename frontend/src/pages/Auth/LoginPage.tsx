import React from 'react'
import { useForm } from 'react-hook-form'
import { useAppDispatch, useAppSelector } from '../../store/hooks'
import { login } from '../../store/slices/authSlice'
import { Navigate, Link } from 'react-router-dom'

export default function LoginPage() {
  const dispatch = useAppDispatch()
  const { register, handleSubmit } = useForm()
  const auth = useAppSelector(s => s.auth)

  const onSubmit = (data: any) => {
    dispatch(login(data))
  }

  if (auth.token) return <Navigate to="/tasks" replace />

  return (
    <div className="min-h-screen flex items-center justify-center p-4">
      <form className="w-full max-w-md bg-white p-6 rounded shadow" onSubmit={handleSubmit(onSubmit)}>
        <h2 className="text-2xl font-bold mb-4">Login</h2>
        <label className="block mb-2">Email</label>
        <input className="w-full p-2 border rounded mb-4" {...register('email', { required: true })} />
        <label className="block mb-2">Password</label>
        <input type="password" className="w-full p-2 border rounded mb-4" {...register('password', { required: true })} />
        <button className="w-full p-2 bg-blue-600 text-white rounded">Login</button>
        <p className="mt-4 text-sm">No account? <Link to="/register" className="text-blue-600">Register</Link></p>
      </form>
    </div>
  )
}
