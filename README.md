## 📄 Project: **MyTasks – Task Management Web App**

### 🎯 Goal

A modern and secure web application that enables users to manage personal tasks.
The project combines a **lightweight .NET backend** with a **React-based frontend**, and authentication is handled through **JWT**.

*(Concept version – the description will be updated once the app is finished.)*

---

## 🧱 Technologies - Concept, aproach my change

### Backend

* **.NET 8 Minimal API**
* **Entity Framework Core** (SQLite)
* **JWT Authentication**
* **Reflection** (e.g., model mapping)
* **Abstraction** with interfaces (`ITaskService`, `IUserService`)
* **Web API**
* **Local functions** within services
* **Localization** support (multi-language)
* **Unit Tests** with FakeItEasy
* Planned hosting on **Azure Free Tier**
* Modular / **microservice-like** architecture
* users will be added via WEB API to simulate external system

### Frontend (SPA)

* **React** (Vite or Create React App)
* **JavaScript (ES6+)** – class-based helpers and models
* **Axios** – HTTP requests
* **React Router** – page navigation
* **localStorage** – JWT storage
* **AntiForgeryToken** support

---

## 🔐 Authentication Flow

1. User logs in via the **Login Page**.
2. On success, the backend issues a **JWT**.
3. The token is stored in `localStorage`.
4. React attaches the JWT in the `Authorization` header for each API request.
5. The backend validates the token and grants access to protected endpoints.

---

## 📄 Pages

### 1. **Dashboard** (`/`)

* Displays the user’s task list
* Mark tasks as complete
* Edit task option

### 2. **Create Task** (`/create`)

* Form for adding new tasks (title, description, due date)

### 3. **Edit Task** (`/edit/:id`)

* Update existing task details

### 4. **Login** (`/login`)

* Username & password form
* On success → redirect to dashboard

---

## 🧩 Key Features

### ✅ Task Management

* Full CRUD operations
* Mark as complete / incomplete
* Filter tasks (e.g., active / completed)

### 🔐 Authentication

* JWT-based login
* Protected routes in React
* `[Authorize]` attribute in backend

### 🧠 Advanced Concepts

* Reflection for DTO mapping
* Abstraction in services and repositories
* JavaScript **classes** for helper logic (`TokenManager`, `TaskModel`)

---

## 📁 Folder Structure (concept)

### Backend (.NET)

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
│   ├── /auth            ← TokenManager, JWT handling
│   ├── App.jsx
│   └── main.jsx
└── package.json
```

Chcesz żebym przygotował do tego też **short version** (np. 3–4 zdania czysto marketingowe), którą możesz wkleić na GitHub i LinkedIn, czy zostawiamy tylko tę pełną techniczną wersję?
