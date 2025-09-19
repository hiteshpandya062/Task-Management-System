import React, { useEffect } from 'react'
import { useAppDispatch } from '../../store/hooks'
import { fetchTasks } from '../../store/slices/tasksSlice'
import { TaskBoard } from './components/TaskBoard'

export default function TasksPage() {
  const dispatch = useAppDispatch()

  useEffect(() => { dispatch(fetchTasks()) }, [dispatch])

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Tasks</h1>
      <TaskBoard />
    </div>
  )
}
