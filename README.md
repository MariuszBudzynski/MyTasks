## 📄 Project: **MyTasks – Task Management Web App**

### 🎯 Goal

A modern and secure web application that enables users to manage personal tasks efficiently. The project combines a **lightweight .NET backend** with a **React-based frontend**, and uses **JWT authentication** for secure access.

> 🧪 *This is a concept version — the description will be updated once the app is finalized.*

> ⚠️ **Note:** The JWT key is intentionally left empty. It is securely stored using User Secrets under the key `"Jwt:Key"`.
>  Key example (stored in MyTasks) =>

```
{
  "Jwt:Key": "this_is_a_very_secure_key_32_chars_long!"
}
```

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
- **In memory Db / SQL Lite db **

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
##  🛠 MyTasks Local Setup
1. Clone the repository
2. Run the application.
3. You will see the login page: <img width="643" height="351" alt="image" src="https://github.com/user-attachments/assets/dae3af77-73d7-4f6d-b501-8cc7ca846fc3" />
4. Two test users are pre-configured:
    - admin / admin123
    - user / user123
5. After login, the dashboard will appear: <img width="884" height="885" alt="image" src="https://github.com/user-attachments/assets/db1f1728-07a3-4d33-adb9-a803e194df54" />
6. The current version uses in-memory data. To switch to the built-in database, comment/uncomment the relevant lines in ServiceRegistration in Program.cs: <img width="737" height="237" alt="image" src="https://github.com/user-attachments/assets/c3b0ac4f-f048-4773-8451-ac6b3484c553" />

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

3. Install the latest Azure Functions Core Tools
4. Run the project in debug mode (func start or via Visual Studio)
5. You should see output similar to: <img width="1117" height="376" alt="Function running preview" src="https://github.com/user-attachments/assets/d82cce60-89d7-4988-924e-0e1680d65374" />
6. Use the displayed link to trigger the function in your browser or via Postman.
