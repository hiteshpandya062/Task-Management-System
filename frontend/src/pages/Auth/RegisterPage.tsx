import React from 'react'
import { useForm } from 'react-hook-form'
import { useAppDispatch } from '../../store/hooks'
import { register as registerAction } from '../../store/slices/authSlice'
import { Link } from 'react-router-dom'

export default function RegisterPage() {
  const dispatch = useAppDispatch()
  const { register, handleSubmit } = useForm()

  const onSubmit = (data: any) => {
    dispatch(registerAction(data))
  }

  return (
    <div className="min-h-screen flex items-center justify-center p-4">
      <form className="w-full max-w-md bg-white p-6 rounded shadow" onSubmit={handleSubmit(onSubmit)}>
        <h2 className="text-2xl font-bold mb-4">Register</h2>
        <input className="w-full p-2 border rounded mb-3" placeholder="Username" {...register('username', { required: true })} />
        <input className="w-full p-2 border rounded mb-3" placeholder="Email" {...register('email', { required: true })} />
        <input className="w-full p-2 border rounded mb-3" placeholder="Password" type="password" {...register('password', { required: true })} />
        <button className="w-full p-2 bg-green-600 text-white rounded">Register</button>
        <p className="mt-3 text-sm">Already have an account? <Link to="/login" className="text-blue-600">Login</Link></p>
      </form>
    </div>
  )
}
