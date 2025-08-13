## 📄 Project Name: **MyTasks – Task Management Web App**

### 🎯 Goal

A modern, secure web application that allows users to manage their personal tasks. It includes authentication using JWT, a lightweight .NET backend, and a React-based frontend.

---
(Concept)

## 🧱 Technologies

### Backend (API)

* **.NET 8 Minimal API**
* **Entity Framework Core** (SQLite)
* **JWT Authentication**
* **Reflection** (e.g., model mapping)
* **Abstraction** via interfaces (`ITaskService`, `IUserService`)
* *Local function/functions*
* *Web API*
* try to host it on Azure (free tier)
* Microservise
* Localizations

### Frontend (SPA)

* **React** (Vite or Create React App)
* **JavaScript (ES6+)** – **class-based** components or helpers where relevant
* **Axios** – for HTTP requests
* **React Router** – for page navigation
* **localStorage** – to store JWT token

---

## 🔐 Authentication Flow

1. User logs in via the **Login Page**.
2. On success, the backend returns a **JWT**.
3. The JWT is saved in `localStorage`.
4. React sends JWT in `Authorization` headers with each API call.
5. Backend validates token and serves secured endpoints.

---

## 📄 Pages

### 1. **Dashboard**

* URL: `/`
* Shows a list of tasks for the logged-in user
* Allows marking tasks as done
* "Edit" button for each task

### 2. **Create Task**

* URL: `/create`
* Form to add a new task (title, description, due date)

### 3. **Edit Task**

* URL: `/edit/:id`
* Allows updating task details

### 4. **Login**

* URL: `/login`
* Username and password form
* On success → redirect to dashboard

---

## 🧩 Key Features

### ✅ Task Management

* CRUD operations on tasks
* Mark as complete/incomplete
* Filter tasks (e.g., active/done)

### 🔐 Auth

* JWT-based login
* Protected routes in React
* Backend validation using `[Authorize]` attribute

### 🧠 Advanced Concepts

* Reflection for DTO mapping
* Abstraction in services and data access
* JavaScript **classes** for helper logic (e.g., token manager, task model)

---

## 📁 Folder Structure (concept)

### Backend

```
/MyTasks.API
├── /Auth
├── /Models
├── /DTOs
├── /Services
├── /Repositories
├── /Middlewares
├── /Extensions
└── Program.cs
```

### Frontend (React)

```
/mytasks-react
├── /src
│   ├── /components
│   ├── /pages
│   ├── /services        ← Axios API calls
│   ├── /utils           ← Class-based helpers
│   ├── /auth            ← TokenManager class etc.
│   ├── App.jsx
│   └── main.jsx
└── package.json
```
