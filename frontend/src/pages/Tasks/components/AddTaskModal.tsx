import React, { useState } from 'react'
import { useAppDispatch } from '../../../store/hooks'
import { createTask } from '../../../store/slices/tasksSlice'

interface Props {
  onClose: () => void
}

export const AddTaskModal: React.FC<Props> = ({ onClose }) => {
  const dispatch = useAppDispatch()
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [priority, setPriority] = useState<'Low' | 'Medium' | 'High'>('Medium')

  const handleSubmit = () => {
    if (!title.trim()) return
    dispatch(createTask({ title, description, status: 'ToDo', priority:1 }))
    onClose()
  }

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-white p-6 rounded w-96">
        <h2 className="font-bold text-lg mb-4">Add Task</h2>
        <input
          className="border p-2 mb-2 w-full"
          placeholder="Title"
          value={title}
          onChange={e => setTitle(e.target.value)}
        />
        <textarea
          className="border p-2 mb-2 w-full"
          placeholder="Description"
          value={description}
          onChange={e => setDescription(e.target.value)}
        />
        <select
          className="border p-2 mb-2 w-full"
          value={priority}
          onChange={e => setPriority(e.target.value as any)}
        >
          <option>Low</option>
          <option>Medium</option>
          <option>High</option>
        </select>
        <div className="flex justify-end mt-4">
          <button className="mr-2 px-3 py-1 border rounded" onClick={onClose}>
            Cancel
          </button>
          <button className="px-3 py-1 bg-green-600 text-white rounded" onClick={handleSubmit}>
            Save
          </button>
        </div>
      </div>
    </div>
  )
}
