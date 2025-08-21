import React from "react";
import ReactDOM from "react-dom/client";
import "./i18n";
import Login from "./pages/login/Login"; // imports components
import Dashboard from "./pages/dashboard/Dashboard";

// find <div id="react-login"> in Login.cshtml and render react
// this concept is used if we want one entry point to render we can split it to several others if needed
const loginRoot = document.getElementById("react-login");
if (loginRoot) {
  ReactDOM.createRoot(loginRoot).render(<Login />);
}

const dashboardRoot = document.getElementById("react-dashboard");
if (dashboardRoot) {
  ReactDOM.createRoot(dashboardRoot).render(<Dashboard />);
}
