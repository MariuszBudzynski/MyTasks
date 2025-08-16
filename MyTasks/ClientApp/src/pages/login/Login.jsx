import React from "react";

export default function Login() {
  return (
    <form method="post" className="space-y-4">
      <div>
        <label>User Name:</label>
        <input type="text" name="Username" className="border p-2 w-full" />
      </div>

      <div>
        <label>Password:</label>
        <input
          type="password"
          name="PasswordHash"
          className="border p-2 w-full"
        />
      </div>

      <button type="submit" className="bg-blue-600 text-white p-2 rounded">
        Login
      </button>
    </form>
  );
}
