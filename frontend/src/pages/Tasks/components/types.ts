export type Status = 'ToDo' | 'InProgress' | 'Done'

export interface Task {
  id: number
  title: string
  description: string
  status: Status
  assigneeName?: string
  assigneeId?: number
  creatorId?: number
  priority?: 'Low' | 'Medium' | 'High'
}
