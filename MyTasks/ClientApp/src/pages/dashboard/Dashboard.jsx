import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

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
    return <p>{t("Loading dashboard...")}</p>;
  }

  const projects = Array.isArray(dashboardData.Projects)
    ? dashboardData.Projects
    : [];
  const tasks = Array.isArray(dashboardData.Tasks) ? dashboardData.Tasks : [];
  const username = dashboardData.Username ?? t("Guest");
  const projectCount = dashboardData.ProjectCount ?? projects.length;
  const taskCount = dashboardData.TaskCount ?? tasks.length;

  return (
    <div id="dashboard-container" className="p-4">
      <h2 className="mb-3">
        {t("Welcome")}, {username}
      </h2>
      <p>
        {t("Projects")}: {projectCount} | {t("Tasks")}: {taskCount}
      </p>

      <h3 className="mt-4">{t("Projects")}</h3>
      <div className="project-list">
        {projects.length > 0 ? (
          projects.map((p) => (
            <div key={p.Id} className="border rounded p-3 mb-2">
              <h4>{p.Name}</h4>
              <p>{p.Description}</p>
              <p>
                {t("Tasks")}: {p.TaskCount} | {t("Completed")}:{" "}
                {p.CompletedTasks}
              </p>
              <button
                className="btn btn-sm btn-primary me-2"
                onClick={() => console.log("Edit project", p.Id, csrfToken)}
              >
                {t("Edit")}
              </button>
              <button
                className="btn btn-sm btn-danger"
                onClick={() => console.log("Delete project", p.Id, csrfToken)}
              >
                {t("Delete")}
              </button>
            </div>
          ))
        ) : (
          <p>{t("No projects available")}</p>
        )}
      </div>

      <h3 className="mt-4">{t("Tasks")}</h3>
      <div className="task-list">
        {tasks.length > 0 ? (
          tasks.map((task) => (
            <div key={task.Id} className="border rounded p-3 mb-2">
              <h5>{task.Title}</h5>
              <p>{task.Description}</p>
              <p>
                {t("Project")}: {task.ProjectName ?? t("N/A")}
                <br />
                {t("Due")}: {task.DueDate ?? t("N/A")}
                <br />
                {t("Completed")}: {task.IsCompleted ? "✅" : "❌"}
              </p>
              {task.LastComment && (
                <p>
                  💬 {task.LastComment} ({task.LastCommentAt ?? t("N/A")})
                </p>
              )}
              <button
                className="btn btn-sm btn-primary me-2"
                onClick={() => console.log("Edit task", task.Id, csrfToken)}
              >
                {t("Edit")}
              </button>
              <button
                className="btn btn-sm btn-danger"
                onClick={() => console.log("Delete task", task.Id, csrfToken)}
              >
                {t("Delete")}
              </button>
            </div>
          ))
        ) : (
          <p>{t("No tasks available")}</p>
        )}
      </div>
    </div>
  );
}
