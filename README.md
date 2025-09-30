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
- **Azurite**

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

3. Install the latest Azure Functions Core Tools
4. Run the project in debug mode (func start or via Visual Studio)
5. You should see output similar to: <img width="1117" height="376" alt="Function running preview" src="https://github.com/user-attachments/assets/d82cce60-89d7-4988-924e-0e1680d65374" />
6. Use the displayed link to trigger the function in your browser or via Postman.
7. To test functions other than HttpTrigger Azurite needs to be run
8. Update the local settings JSON to :      "AzureWebJobsStorage": "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFeqCnbn...==;BlobEndpoint=http://127.0.0.1:10010/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10011/devstoreaccount1;TableEndpoint=http://127.0.0.1:10012/devstoreaccount1;",
9. Go to  Azurite folder in solution and use this command npx azurite --queuePort 10011. This will start the server emulation.
