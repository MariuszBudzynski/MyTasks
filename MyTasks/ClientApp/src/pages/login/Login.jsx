import React from "react";

export default function Login() {
  return (
    <div className="max-w-sm mx-auto mt-10 space-y-4">
      <div className="flex items-center">
        <label className="mr-4 w-24">User Name:</label>
        <input type="text" name="Username" className="border p-2 flex-1" />
      </div>

      <div className="flex items-center">
        <label className="mr-4 w-24">Password:</label>
        <input
          type="password"
          name="PasswordHash"
          className="border p-2 flex-1"
        />
      </div>

      <button
        type="button"
        className="bg-blue-600 text-white p-2 w-full mt-4"
        onClick={() => {
          // AJAX goes here
          console.log("Login clicked");
        }}
      >
        Login
      </button>
    </div>
  );
}
