import React from "react";

export default function Login() {
  return (
    <div
      id="login-container"
      style={{
        display: "flex",
        flexDirection: "column",
        gap: "15px",
        width: "300px",
        margin: "50px auto",
      }}
    >
      <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
        <label>User Name:</label>
        <input type="text" name="Username" />
      </div>

      <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
        <label>Password:</label>
        <input type="password" name="PasswordHash" />
      </div>

      <button
        type="button"
        style={{ padding: "8px 12px" }}
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
