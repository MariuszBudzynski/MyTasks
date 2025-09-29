## 📄 Project: **MyTasks – Task Management Web App**

### 🎯 Goal

A modern and secure web application that enables users to manage personal tasks efficiently. The project combines a **lightweight .NET backend** with a **React-based frontend**, and uses **JWT authentication** for secure access.

> 🧪 *This is a concept version — the description will be updated once the app is finalized.*

> ⚠️ **Note:** The JWT key is intentionally left empty. It is securely stored using User Secrets under the key `"Jwt:Key"`.

---

## 🧱 Technologies

### 🔧 Backend

- **.NET 8 Minimal API**
- **Entity Framework Core** (SQLite)
- **JWT Authentication**
- **Interface-Based Abstraction**
- **Web API Endpoints**
- **Unit Testing** with FakeItEasy
- **Razor Pages**
- **SOLID Principles**
- **Dependency Injection**
- **Structured Logging & Error Handling**
- **Asynchronous Programming** (`async` / `await`)
- **Data Validation & Error Responses**
- **Local Functions**

### 🎨 Frontend (SPA)

- **React** (Vite)
- **Hooks for State Management** (`useState`, `useEffect`)
- **Localization** via `react-i18next`
- **JavaScript (ES6+)**
- **AJAX / Fetch API** for HTTP requests
- **CSRF Protection** (Anti-Forgery Token)
- **Component-Based Architecture**
- **Responsive Design** (CSS Modules, Flexbox/Grid)
- **Form Handling & Validation**
- **Error Handling & User Feedback**

---

## ⚙️ MyTasks.Functions Configuration

To run the Azure Functions project locally:

1. **Set `MyTasks.Functions` as the startup project** in your solution.
2. Ensure you have a `local.settings.json` file with the following configuration:

   ```json
   {
     "IsEncrypted": false,
     "Values": {
       "AzureWebJobsStorage": "UseDevelopmentStorage=true",
       "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
       "Functions:Worker:HostEndpoint": "127.0.0.1:9090"
     }
   }
```
3. Install the latest Azure Functions Core Tools
4. Run the project in debug mode (func start or via Visual Studio)
