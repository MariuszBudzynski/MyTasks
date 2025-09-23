import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import styles from "./Login.module.css";

function InputField({ label, type, name, value, onChange }) {
  return (
    <div className={styles.row}>
      <label htmlFor={name} className={styles.label}>
        {label}:
      </label>
      <input
        type={type}
        id={name}
        name={name}
        className={styles.input}
        value={value}
        onChange={onChange}
      />
    </div>
  );
}

export default function Login() {
  const { t } = useTranslation();

  const [formData, setFormData] = useState({ username: "", password: "" });
  const [errorMessage, setErrorMessage] = useState("");
  const [csrfToken, setCsrfToken] = useState("");

  useEffect(() => {
    const token = document
      .getElementById("react-login")
      .getAttribute("data-csrf-token");
    setCsrfToken(token);
  }, []);

  const handleLogin = async () => {
    setErrorMessage("");

    try {
      const response = await fetch("/Login?handler=Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify({
          username: formData.username,
          password: formData.password,
        }),
      });

      if (response.ok) {
        window.location.href = "/Dashboard";
      } else {
        const data = await response.json();
        setErrorMessage(data.message || "Login failed");
      }
    } catch (err) {
      setErrorMessage("Network error. Please try again.");
    }
  };

  return (
    <div id="login-container" className={styles.container}>
      <InputField
        label={t("username")}
        type="text"
        name="Username"
        value={formData.username}
        onChange={(e) => setFormData({ ...formData, username: e.target.value })}
      />

      <InputField
        label={t("password")}
        type="password"
        name="PasswordHash"
        value={formData.password}
        onChange={(e) => setFormData({ ...formData, password: e.target.value })}
      />

      {errorMessage && (
        <div className={styles.errorMessage}>{errorMessage}</div>
      )}

      <button type="button" className={styles.button} onClick={handleLogin}>
        {t("login")}
      </button>
    </div>
  );
}
