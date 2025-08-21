import { useTranslation } from "react-i18next";

const styles = {};

//change everything later
export default function Main() {
  const { t } = useTranslation();

  const csrfToken = document // <-- change this to point to proper page
    .getElementById("react-login")
    .getAttribute("data-csrf-token");

  return (
    <div id="dashboard-container">
      <h1 class="display-4">Table goes here</h1>
    </div>
  );
}
