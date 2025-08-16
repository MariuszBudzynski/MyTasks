import React from "react";

const styles = {
  container: {
    display: "flex",
    flexDirection: "column",
    gap: "15px",
    width: "400px",
    margin: "50px auto",
    padding: "20px",
    border: "1px solid #ccc",
    borderRadius: "8px",
    backgroundColor: "#f9f9f9",
  },
  row: {
    display: "flex",
    alignItems: "center",
    flexDirection: "row",
    justifyContent: "flex-end",
  },
  usernameRow: {
    gap: "10px",
  },
  passwordRow: {
    gap: "22px",
  },
  input: {
    flex: 1,
    padding: "6px 8px",
    borderRadius: "4px",
    border: "1px solid #ccc",
  },
  button: {
    padding: "8px 12px",
    borderRadius: "4px",
    border: "none",
    backgroundColor: "#007bff",
    color: "#fff",
    cursor: "pointer",
    width: "269px",
    marginLeft: "auto",
  },
};

export default function Login() {
  return (
    <div id="login-container" style={styles.container}>
      <div style={{ ...styles.row, ...styles.usernameRow }}>
        <label>User Name:</label>
        <input type="text" name="Username" style={styles.input} />
      </div>

      <div style={{ ...styles.row, ...styles.passwordRow }}>
        <label>Password:</label>
        <input type="password" name="PasswordHash" style={styles.input} />
      </div>

      <button
        type="button"
        style={styles.button}
        onClick={() => {
          console.log("Login clicked");
        }}
      >
        Login
      </button>
    </div>
  );
}
