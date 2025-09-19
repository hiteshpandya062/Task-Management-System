import React, { useState, useEffect } from 'react'
import { useAppDispatch, useAppSelector } from '../../../store/hooks'
import { AddTaskModal } from './AddTaskModal'
import { SortableItem } from './SortableItem'
import { fetchTasks, updateTask, Task } from '../../../store/slices/tasksSlice'
import { DndContext, PointerSensor, useSensor, useSensors, DragEndEvent, closestCenter } from '@dnd-kit/core'
import { SortableContext, verticalListSortingStrategy } from '@dnd-kit/sortable'

type GroupedTasks = { ToDo: Task[]; InProgress: Task[]; Done: Task[] }

export const TaskBoard: React.FC = () => {
  const dispatch = useAppDispatch()
  const tasks = useAppSelector((s) => s.tasks.items)
  const [showModal, setShowModal] = useState(false)

  const sensors = useSensors(useSensor(PointerSensor))

  useEffect(() => { dispatch(fetchTasks()) }, [dispatch])

  const groups: GroupedTasks = {
    ToDo: tasks.filter((t) => t.status === 'ToDo'),
    InProgress: tasks.filter((t) => t.status === 'InProgress'),
    Done: tasks.filter((t) => t.status === 'Done')
  }

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event
    if (!over) return

    const taskId = Number(active.id)
    const newStatus = over.data.current?.status as Task['status'] | undefined
    const task = tasks.find((t) => t.id === taskId)

    if (task && newStatus && task.status !== newStatus) {
      dispatch(updateTask({ id: taskId, payload: { ...task, status: newStatus } }))
    }
  }

  return (
    <div>
      <div className="flex justify-between items-center mb-4">
        <h1 className="text-2xl font-bold">Task Board</h1>
        <button className="px-3 py-1 bg-blue-600 text-white rounded" onClick={() => setShowModal(true)}>
          + Add Task
        </button>
      </div>

      {showModal && <AddTaskModal onClose={() => setShowModal(false)} />}

      <DndContext sensors={sensors} collisionDetection={closestCenter} onDragEnd={handleDragEnd}>
        <div className="grid grid-cols-3 gap-4">
          {Object.entries(groups).map(([status, list]) => (
            <SortableContext key={status} items={list.map((t) => t.id!)} strategy={verticalListSortingStrategy}>
              <div data-status={status} className="bg-white p-4 rounded shadow min-h-[300px]">
                <h2 className="font-semibold mb-3">{status}</h2>
                <div className="space-y-2">
                  {list.map((task) => (
                    <SortableItem key={task.id} task={task} status={status as Task['status']} />
                  ))}
                </div>
              </div>
            </SortableContext>
          ))}
        </div>
      </DndContext>
    </div>
  )
}
