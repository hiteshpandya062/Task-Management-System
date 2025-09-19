import React from 'react'
import { Outlet, Navigate } from 'react-router-dom'
import { useAppSelector } from '../store/hooks'

export const PrivateRoute: React.FC = () => {
  const token = useAppSelector(s => s.auth.token)
  if (!token) return <Navigate to="/login" replace />
  return <Outlet />
}
