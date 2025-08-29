import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

const styles = {
  container: {
    maxWidth: "900px",
    margin: "40px auto",
    padding: "20px",
    backgroundColor: "#f9f9f9",
    borderRadius: "10px",
    boxShadow: "0 4px 10px rgba(0,0,0,0.1)",
    fontFamily: "Arial, sans-serif",
  },
  header: {
    marginBottom: "20px",
    textAlign: "center",
  },
  section: {
    marginTop: "30px",
  },
  list: {
    display: "grid",
    gap: "15px",
  },
  card: {
    backgroundColor: "#fff",
    padding: "15px",
    borderRadius: "8px",
    border: "1px solid #ddd",
    boxShadow: "0 2px 5px rgba(0,0,0,0.05)",
  },
  cardTitle: {
    fontSize: "18px",
    fontWeight: "600",
    marginBottom: "8px",
  },
  cardText: {
    fontSize: "14px",
    marginBottom: "6px",
    color: "#444",
  },
  buttons: {
    display: "flex",
    gap: "10px",
    marginTop: "10px",
  },
  button: {
    padding: "6px 12px",
    borderRadius: "5px",
    border: "none",
    cursor: "pointer",
    fontSize: "14px",
    fontWeight: "500",
  },
  btnPrimary: {
    backgroundColor: "#007bff",
    color: "#fff",
  },
  btnDanger: {
    backgroundColor: "#dc3545",
    color: "#fff",
  },
  empty: {
    fontStyle: "italic",
    color: "#666",
  },
};

export default function Dashboard() {
  const { t } = useTranslation();
  const [dashboardData, setDashboardData] = useState({});
  const [csrfToken, setCsrfToken] = useState(null);

  useEffect(() => {
    const dashboardEl = document.getElementById("react-dashboard");
    if (dashboardEl) {
      const csrfAttr = dashboardEl.getAttribute("data-csrf-token");
      if (csrfAttr) setCsrfToken(csrfAttr);
    }

    const dashboardScript = document.getElementById("dashboard-data");
    if (dashboardScript) {
      try {
        const parsed = JSON.parse(dashboardScript.textContent);
        setDashboardData(parsed);
      } catch (e) {
        console.error("Error parsing dashboard data:", e);
      }
    }
  }, []);

  if (!dashboardData || Object.keys(dashboardData).length === 0) {
    return <p style={{ textAlign: "center" }}>{t("Loading dashboard...")}</p>;
  }

  const projects = Array.isArray(dashboardData.Projects)
    ? dashboardData.Projects
    : [];
  const tasks = Array.isArray(dashboardData.Tasks) ? dashboardData.Tasks : [];
  const username = dashboardData.Username ?? t("Guest");
  const projectCount = dashboardData.ProjectCount ?? projects.length;
  const taskCount = dashboardData.TaskCount ?? tasks.length;

  return (
    <div style={styles.container}>
      <div style={styles.header}>
        <h2>
          {t("Welcome")}, {username}
        </h2>
        <p>
          {t("Projects")}: {projectCount} | {t("Tasks")}: {taskCount}
        </p>
      </div>

      {/* Projects Section */}
      <div style={styles.section}>
        <h3>{t("Projects")}</h3>
        <div style={styles.list}>
          {projects.length > 0 ? (
            projects.map((p) => (
              <div key={p.Id} style={styles.card}>
                <div style={styles.cardTitle}>{p.Name}</div>
                <p style={styles.cardText}>{p.Description}</p>
                <p style={styles.cardText}>
                  {t("Tasks")}: {p.TaskCount} | {t("Completed")}:{" "}
                  {p.CompletedTasks}
                </p>
                <div style={styles.buttons}>
                  <button
                    style={{ ...styles.button, ...styles.btnPrimary }}
                    onClick={() => console.log("Edit project", p.Id, csrfToken)}
                  >
                    {t("Edit")}
                  </button>
                  <button
                    style={{ ...styles.button, ...styles.btnDanger }}
                    onClick={() =>
                      console.log("Delete project", p.Id, csrfToken)
                    }
                  >
                    {t("Delete")}
                  </button>
                </div>
              </div>
            ))
          ) : (
            <p style={styles.empty}>{t("No projects available")}</p>
          )}
        </div>
      </div>

      {/* Tasks Section */}
      <div style={styles.section}>
        <h3>{t("Tasks")}</h3>
        <div style={styles.list}>
          {tasks.length > 0 ? (
            tasks.map((task) => (
              <div key={task.Id} style={styles.card}>
                <div style={styles.cardTitle}>{task.Title}</div>
                <p style={styles.cardText}>{task.Description}</p>
                <p style={styles.cardText}>
                  {t("Project")}: {task.ProjectName ?? t("N/A")}
                  <br />
                  {t("Due")}: {task.DueDate ?? t("N/A")}
                  <br />
                  {t("Completed")}: {task.IsCompleted ? "✅" : "❌"}
                </p>
                {task.LastComment && (
                  <p style={styles.cardText}>
                    💬 {task.LastComment} ({task.LastCommentAt ?? t("N/A")})
                  </p>
                )}
                <div style={styles.buttons}>
                  <button
                    style={{ ...styles.button, ...styles.btnPrimary }}
                    onClick={() => console.log("Edit task", task.Id, csrfToken)}
                  >
                    {t("Edit")}
                  </button>
                  <button
                    style={{ ...styles.button, ...styles.btnDanger }}
                    onClick={() =>
                      console.log("Delete task", task.Id, csrfToken)
                    }
                  >
                    {t("Delete")}
                  </button>
                </div>
              </div>
            ))
          ) : (
            <p style={styles.empty}>{t("No tasks available")}</p>
          )}
        </div>
      </div>
    </div>
  );
}
