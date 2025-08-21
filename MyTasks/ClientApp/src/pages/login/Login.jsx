import React, { useState } from "react";
import { useTranslation } from "react-i18next";

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
    display: "grid",
    gridTemplateColumns: "1fr 1fr",
    alignItems: "center",
  },
  label: {
    textAlign: "left",
    fontWeight: "500",
  },
  input: {
    padding: "6px 8px",
    borderRadius: "4px",
    border: "1px solid #ccc",
    width: "100%",
  },
  button: {
    gridColumn: "1 / 2",
    padding: "8px 12px",
    borderRadius: "4px",
    border: "none",
    backgroundColor: "#007bff",
    color: "#fff",
    cursor: "pointer",
    width: "100%",
  },
};

export default function Login() {
  const { t } = useTranslation();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  //we retrive this from Login.cshtml.cs that we set up by conventions
  const csrfToken = document
    .getElementById("react-login")
    .getAttribute("data-csrf-token");

  // Fetch pattern for Razor Pages:
  // /PageName?handler=HandlerName
  // - PageName = name of the .cshtml page (without extension)
  // - HandlerName = name from OnPostHandlerAsync / OnGetHandlerAsync
  //   e.g., OnPostLoginAsync → handler=Login ASP "cuts" the OnPost and Async by convention
  const handleLogin = async () => {
    const response = await fetch("/Login?handler=Login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "X-CSRF-TOKEN": csrfToken,
      },
      credentials: "include", //sends and retrives cookie
      body: JSON.stringify({
        username,
        password,
      }),
    });

    if (response.ok) {
      console.log("Login success!");
      setErrorMessage("");
      window.location.href = "/Dashboard";
    } else {
      const data = await response.json();
      setErrorMessage(data.message || "Login failed");
    }
  };

  return (
    <div id="login-container" style={styles.container}>
      <div style={styles.row}>
        <label style={styles.label}>{t("username")}:</label>
        <input
          type="text"
          name="Username"
          style={styles.input}
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </div>

      <div style={styles.row}>
        <label style={styles.label}>{t("password")}:</label>
        <input
          type="password"
          name="PasswordHash"
          style={styles.input}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>

      {errorMessage && (
        <div style={{ color: "red", textAlign: "center" }}>{errorMessage}</div>
      )}

      <button type="button" style={styles.button} onClick={handleLogin}>
        {t("login")}
      </button>
    </div>
  );
}
