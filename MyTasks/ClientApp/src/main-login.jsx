import React from "react";
import ReactDOM from "react-dom/client";
import Login from "./pages/login/Login"; // imports components

// find <div id="react-login"> in Login.cshtml and render react
ReactDOM.createRoot(document.getElementById("react-login")).render(<Login />);
