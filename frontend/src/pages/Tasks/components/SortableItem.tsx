import React from 'react'
import { useSortable } from '@dnd-kit/sortable'
import { CSS } from '@dnd-kit/utilities'
import { Task } from '../../../store/slices/tasksSlice'
import { useAppSelector, useAppDispatch } from '../../../store/hooks'
import { deleteTask } from '../../../store/slices/tasksSlice'
interface Props {
    task: any
    status: Task['status']
}

export const SortableItem: React.FC<Props> = ({ task }) => {
    const { attributes, listeners, setNodeRef, transform, transition } = useSortable({ id: task.id! })
    const style = { transform: CSS.Transform.toString(transform), transition }
    const dispatch = useAppDispatch()
    const role = useAppSelector(state => state.auth.user?.role)
    return (
        <div ref={setNodeRef} {...attributes} {...listeners} style={style} className="p-3 border rounded bg-gray-50 hover:shadow">
            <h3 className="font-medium">{task.title}</h3>
            <p className="text-sm text-gray-600">{task.description}</p>
            {task.assigneeName && <div className="text-xs mt-1 text-gray-500">Assignee: {task.assigneeName}</div>}
            <button
                onClick={() => dispatch(deleteTask(task.id))}
                className="mt-2 text-red-500 text-xs hover:underline"
            >
                Delete
            </button>
        </div>
    )
}
