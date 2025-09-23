import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import styles from "./Login.module.css";

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
    <div id="login-container" className={styles.container}>
      <div className={styles.row}>
        <label className={styles.label}>{t("username")}:</label>
        <input
          type="text"
          name="Username"
          className={styles.input}
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </div>

      <div className={styles.row}>
        <label className={styles.label}>{t("password")}:</label>
        <input
          type="password"
          name="PasswordHash"
          className={styles.input}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>

      {errorMessage && (
        <div style={{ color: "red", textAlign: "center" }}>{errorMessage}</div>
      )}

      <button type="button" className={styles.button} onClick={handleLogin}>
        {t("login")}
      </button>
    </div>
  );
}
