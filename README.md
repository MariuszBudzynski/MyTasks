## 📄 Project: **MyTasks – Task Management Web App**

### 🎯 Goal

A modern and secure web application that enables users to manage personal tasks.
The project combines a **lightweight .NET backend** with a **React-based frontend**, and authentication is handled through **JWT**.

*(Concept version – the description will be updated once the app is finished.)*

**IMPORTANT** → The JWT key is empty because it is configured to be stored in User Secrets under "Jwt:Key".

---

## 🧱 Technologies

### Backend

- **.NET 8 Minimal API**
- **Entity Framework Core** (SQLite)
- **JWT Authentication**
- **Abstraction via Interfaces**
- **Web API Endpoints**
- **Unit Testing** with FakeItEasy
- **Razor Pages**
- **SOLID Principles**
- **Dependency Injection**
- **Logging & Error Handling**
- **Asynchronous Programming** (`async` / `await`)
- **Data Validation & Error Responses**
- **Local Functions**

### Frontend (SPA)

- **React** (Vite)
- **Hooks for State Management** (`useState`, `useEffect`)
- **Localization** (multi-language support via `react-i18next`)
- **JavaScript (ES6+)**
- **AJAX / Fetch API** for HTTP requests
- **Anti-Forgery Token** support (CSRF protection)
- **Component-Based Architecture**
- **Responsive Design** (CSS Modules, Flexbox/Grid)
- **Form Handling & Validation**
- **Error Handling & User Feedback**

  ## 🧱 MyTasks.Functions config
  1) Set MyTasks.Functions as startup project
  2) it is reqired to have a local.settings.json with similar configuration
     
  {
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "Functions:Worker:HostEndpoint": "127.0.0.1:9090"
  }
}
3) install latest Azure Functions Core tools
4) run with debug
5) you should see <img width="1117" height="376" alt="image" src="https://github.com/user-attachments/assets/d82cce60-89d7-4988-924e-0e1680d65374" />
6) use the link to fire up the function
