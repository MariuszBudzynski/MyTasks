import React from "react";
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
    display: "flex",
    alignItems: "center",
    flexDirection: "row",
    justifyContent: "space-between",
  },
  usernameRow: {
    gap: "10px",
  },
  passwordRow: {
    gap: "10px",
  },
  label: {
    width: "200px",
    display: "inline-block",
    textAlign: "right",
    fontWeight: "500",
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
    width: "200px",
    marginLeft: "auto",
  },
};

export default function Login() {
  const { t } = useTranslation();

  return (
    <div id="login-container" style={styles.container}>
      <div style={{ ...styles.row, ...styles.usernameRow }}>
        <label style={styles.label}>{t("username")}:</label>
        <input type="text" name="Username" style={styles.input} />
      </div>

      <div style={{ ...styles.row, ...styles.passwordRow }}>
        <label style={styles.label}>{t("password")}:</label>
        <input type="password" name="PasswordHash" style={styles.input} />
      </div>

      <button
        type="button"
        style={styles.button}
        onClick={() => {
          console.log("Login clicked");
        }}
      >
        {t("login")}
      </button>
    </div>
  );
}
