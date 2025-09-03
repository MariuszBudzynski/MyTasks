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
    justifyContent: "center",
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
  modalOverlay: {
    position: "fixed",
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0,0,0,0.5)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    zIndex: 1000,
  },
  modal: {
    backgroundColor: "#fff",
    padding: "20px",
    borderRadius: "10px",
    width: "400px",
    maxWidth: "90%",
    boxShadow: "0 4px 10px rgba(0,0,0,0.2)",
  },
  modalInput: {
    width: "100%",
    padding: "8px",
    margin: "8px 0",
    border: "1px solid #ccc",
    borderRadius: "5px",
  },
  modalActions: {
    display: "flex",
    justifyContent: "flex-end",
    gap: "10px",
    marginTop: "10px",
  },
};

export default function Dashboard() {
  const { t } = useTranslation();
  const [dashboardData, setDashboardData] = useState({});
  const [csrfToken, setCsrfToken] = useState(null);

  const [showProjectModal, setShowProjectModal] = useState(false);
  const [newProject, setNewProject] = useState({ name: "", description: "" });
  const [errors, setErrors] = useState({ name: "", description: "" });
  const [touched, setTouched] = useState({ name: false, description: false });
  const [submitting, setSubmitting] = useState(false);
  const [submitError, setSubmitError] = useState("");

  const validate = (p) => ({
    name: p.name.trim() ? "" : t("required"),
    description: p.description.trim() ? "" : t("required"),
  });
  const isValid =
    !validate(newProject).name && !validate(newProject).description;

  const handleChange = (field, value) => {
    const next = { ...newProject, [field]: value };
    setNewProject(next);
    setTouched((x) => ({ ...x, [field]: true }));
    setErrors(validate(next));
  };

  const handleSubmitProject = async () => {
    const v = validate(newProject);
    if (v.name || v.description) {
      setTouched({ name: true, description: true });
      setErrors(v);
      return;
    }
    setSubmitting(true);
    setSubmitError("");

    try {
      const res = await fetch("/api/project", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken ?? "",
        },
        credentials: "include",
        body: JSON.stringify({
          Name: newProject.name,
          Description: newProject.description,
        }),
      });

      if (!res.ok) {
        const msg = (await res.text()) || t("error_saving");
        setSubmitError(msg);
        setSubmitting(false);
        return;
      }

      const created = await res.json();
      setDashboardData((prev) => {
        const next = { ...prev };
        const newCard = {
          Id: created.id,
          Name: created.name,
          Description: created.description,
          TaskCount: 0,
          CompletedTasks: 0,
        };
        next.Projects = Array.isArray(prev.Projects)
          ? [...prev.Projects, newCard]
          : [newCard];
        next.ProjectCount = (prev.ProjectCount ?? 0) + 1;
        return next;
      });

      setShowProjectModal(false);
      setNewProject({ name: "", description: "" });
      setTouched({ name: false, description: false });
      setErrors({ name: "", description: "" });
    } catch (e) {
      setSubmitError(t("error_saving"));
    } finally {
      setSubmitting(false);
    }
  };

  const handleCancelProject = () => {
    setShowProjectModal(false);
    setNewProject({ name: "", description: "" });
    setTouched({ name: false, description: false });
    setErrors({ name: "", description: "" });
    setSubmitError("");
    setSubmitting(false);
  };

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
    return <p style={{ textAlign: "center" }}>{t("loading")}</p>;
  }

  const projects = Array.isArray(dashboardData.Projects)
    ? dashboardData.Projects
    : [];
  const tasks = Array.isArray(dashboardData.Tasks) ? dashboardData.Tasks : [];
  const username = dashboardData.Username ?? t("guest");
  const projectCount = dashboardData.ProjectCount ?? projects.length;
  const taskCount = dashboardData.TaskCount ?? tasks.length;

  return (
    <div style={styles.container}>
      <div style={styles.header}>
        <h2>
          {t("welcome")}, {username}
        </h2>
        <p>
          {t("projects")}: {projectCount} | {t("tasks")}: {taskCount}
        </p>
      </div>

      <div style={styles.buttons}>
        <button
          style={{ ...styles.button, ...styles.btnPrimary }}
          onClick={() => setShowProjectModal(true)}
        >
          ➕ {t("add_project")}
        </button>
        <button
          style={{ ...styles.button, ...styles.btnPrimary }}
          onClick={() => console.log("TODO: open task modal")}
        >
          ➕ {t("add_task")}
        </button>
      </div>

      <div style={styles.section}>
        <h3>{t("projects")}</h3>
        <div style={styles.list}>
          {projects.length > 0 ? (
            projects.map((p) => (
              <div key={p.Id} style={styles.card}>
                <div style={styles.cardTitle}>{p.Name}</div>
                <p style={styles.cardText}>{p.Description}</p>
                <p style={styles.cardText}>
                  {t("tasks")}: {p.TaskCount} | {t("completed")}:{" "}
                  {p.CompletedTasks}
                </p>
                <div style={styles.buttons}>
                  <button
                    style={{ ...styles.button, ...styles.btnPrimary }}
                    onClick={() => console.log("Edit project", p.Id, csrfToken)}
                  >
                    {t("edit")}
                  </button>
                  <button
                    style={{ ...styles.button, ...styles.btnDanger }}
                    onClick={() =>
                      console.log("Delete project", p.Id, csrfToken)
                    }
                  >
                    {t("delete")}
                  </button>
                </div>
              </div>
            ))
          ) : (
            <p style={styles.empty}>{t("no_projects")}</p>
          )}
        </div>
      </div>

      <div style={styles.section}>
        <h3>{t("tasks")}</h3>
        <div style={styles.list}>
          {tasks.length > 0 ? (
            tasks.map((task) => (
              <div key={task.Id} style={styles.card}>
                <div style={styles.cardTitle}>{task.Title}</div>
                <p style={styles.cardText}>{task.Description}</p>
                <p style={styles.cardText}>
                  {t("project")}: {task.ProjectName ?? t("n_a")}
                  <br />
                  {t("due")}: {task.DueDate ?? t("n_a")}
                  <br />
                  {t("completed")}: {task.IsCompleted ? "✅" : "❌"}
                </p>
                {task.LastComment && (
                  <p style={styles.cardText}>
                    💬 {task.LastComment} ({task.LastCommentAt ?? t("n_a")})
                  </p>
                )}
                <div style={styles.buttons}>
                  <button
                    style={{ ...styles.button, ...styles.btnPrimary }}
                    onClick={() => console.log("Edit task", task.Id, csrfToken)}
                  >
                    {t("edit")}
                  </button>
                  <button
                    style={{ ...styles.button, ...styles.btnDanger }}
                    onClick={() =>
                      console.log("Delete task", task.Id, csrfToken)
                    }
                  >
                    {t("delete")}
                  </button>
                </div>
              </div>
            ))
          ) : (
            <p style={styles.empty}>{t("no_tasks")}</p>
          )}
        </div>
      </div>

      {showProjectModal && (
        <div style={styles.modalOverlay}>
          <div style={styles.modal}>
            <h3>{t("add_project")}</h3>
            <input
              type="text"
              placeholder={t("project_name")}
              value={newProject.name}
              onChange={(e) => handleChange("name", e.target.value)}
              onBlur={() => setTouched((x) => ({ ...x, name: true }))}
              style={styles.modalInput}
            />
            {touched.name && errors.name && (
              <div style={{ color: "#dc3545", fontSize: 13 }}>
                {errors.name}
              </div>
            )}
            <textarea
              placeholder={t("project_description")}
              value={newProject.description}
              onChange={(e) => handleChange("description", e.target.value)}
              onBlur={() => setTouched((x) => ({ ...x, description: true }))}
              style={styles.modalInput}
            />
            {touched.description && errors.description && (
              <div style={{ color: "#dc3545", fontSize: 13 }}>
                {errors.description}
              </div>
            )}
            {submitError && (
              <div style={{ color: "#dc3545", fontSize: 13, marginTop: 6 }}>
                {submitError}
              </div>
            )}
            <div style={styles.modalActions}>
              <button
                style={{ ...styles.button, ...styles.btnPrimary }}
                onClick={handleSubmitProject}
                disabled={!isValid || submitting}
              >
                {submitting ? t("saving") : t("ok")}
              </button>
              <button
                style={{ ...styles.button, ...styles.btnDanger }}
                onClick={handleCancelProject}
                disabled={submitting}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
