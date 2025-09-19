import React, { useEffect, useState } from 'react'
import api from '../../api/axios'

export default function UsersPage() {
  const [users, setUsers] = useState<any[]>([])

  useEffect(() => {
    api.get('/api/users').then(r => setUsers(r.data)).catch(() => setUsers([]))
  }, [])

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Users</h1>
      <ul>
        {users.map(u => <li key={u.id} className="mb-2">{u.username} ({u.email})</li>)}
      </ul>
    </div>
  )
}
