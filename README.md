## 📄 Project: **MyTasks – Task Management Web App**

### 🎯 Goal

A modern and secure web application that enables users to manage personal tasks efficiently. The project combines a **lightweight .NET backend** with a **React-based frontend**, and uses **JWT authentication** for secure access.

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
- **In memory Db / SQL Lite db**

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
## 🛠 MyTasks Local Setup

1. Clone the repository.
2. Run the application.
3. You will see the login page:  
   ![Login Page](https://github.com/user-attachments/assets/dae3af77-73d7-4f6d-b501-8cc7ca846fc3)
4. Two test users are pre-configured:
    - **admin** / **admin123**
    - **user** / **user123**
5. After login, the dashboard will appear:  
   ![Dashboard](https://github.com/user-attachments/assets/db1f1728-07a3-4d33-adb9-a803e194df54)
6. By default, the application runs with in-memory data.
  To enable the built-in database, open Program.cs, locate the ServiceRegistration, and set UseInMemory to false.

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
