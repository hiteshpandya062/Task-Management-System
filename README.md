# Task Management System

A full-stack **Task Management System** built with:

- **Frontend:** React 19+, Vite, TypeScript, Tailwind CSS  
- **Backend:** .NET 8 Web API (C#)  
- **State Management:** Redux Toolkit  
- **Authentication:** JWT with Role-based Authorization  

---

## 🚀 Getting Started

### Backend Setup

```bash
cd backend/TaskManagementAPI
dotnet restore
dotnet build
dotnet run
```

API available at:
```
http://localhost:7134
```

---

### Frontend Setup

```bash
cd frontend
npm install
```

Create `.env` file:
```env
VITE_API_URL=http://localhost:7134
```

Run dev server:
```bash
npm run dev
```

App available at:
```
http://localhost:5173
```

---

## 🔑 Features

- **Authentication & Authorization**
  - JWT-based login & registration  
  - Role-based access (Admin / User)  

- **Task Management**
  - Task CRUD (Create, Update, Delete, Get)  
  - Tasks grouped by status (ToDo, InProgress, Done)  
  - Drag & drop task management  

- **UI/UX**
  - Responsive Tailwind UI  
  - Kanban-style task board  

---

## 🛠 Tech Stack

- **Frontend:** React 19+, Vite, TypeScript, Tailwind CSS, Redux Toolkit  
- **Backend:** .NET 8 Web API, Entity Framework Core  
- **Authentication:** JWT Bearer Tokens  
- **Database:** InMemory(SQL) (configurable)  
