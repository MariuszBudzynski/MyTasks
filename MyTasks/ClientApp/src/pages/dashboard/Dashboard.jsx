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
  header: { marginBottom: "20px", textAlign: "center" },
  section: { marginTop: "30px" },
  list: { display: "grid", gap: "15px" },
  card: {
    backgroundColor: "#fff",
    padding: "15px",
    borderRadius: "8px",
    border: "1px solid #ddd",
    boxShadow: "0 2px 5px rgba(0,0,0,0.05)",
  },
  cardTitle: { fontSize: "18px", fontWeight: "600", marginBottom: "8px" },
  cardText: { fontSize: "14px", marginBottom: "6px", color: "#444" },
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
  btnPrimary: { backgroundColor: "#007bff", color: "#fff" },
  btnDanger: { backgroundColor: "#dc3545", color: "#fff" },
  empty: { fontStyle: "italic", color: "#666" },
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
  errorText: {
    color: "red",
    fontSize: "12px",
    marginTop: "-5px",
    marginBottom: "5px",
  },
};

export default function Dashboard() {
  const { t } = useTranslation();
  const [dashboardData, setDashboardData] = useState({});
  const [csrfToken, setCsrfToken] = useState(null);

  const [showProjectModal, setShowProjectModal] = useState(false);
  const [newProject, setNewProject] = useState({ name: "", description: "" });
  const [touched, setTouched] = useState({ name: false, description: false });
  const [errors, setErrors] = useState({});
  const [submitting, setSubmitting] = useState(false);
  const [submitError, setSubmitError] = useState("");

  const [showEditProjectModal, setShowEditProjectModal] = useState(false);
  const [editProject, setEditProject] = useState({
    id: "",
    name: "",
    description: "",
  });
  const [editTouched, setEditTouched] = useState({
    name: false,
    description: false,
  });
  const [editErrors, setEditErrors] = useState({});
  const [submittingEdit, setSubmittingEdit] = useState(false);
  const [submitEditError, setSubmitEditError] = useState("");

  const [showTaskModal, setShowTaskModal] = useState(false);
  const [newTask, setNewTask] = useState({
    title: "",
    description: "",
    dueDate: "",
    projectId: "",
    isCompleted: false,
  });
  const [touchedTask, setTouchedTask] = useState({ title: false });
  const [errorsTask, setErrorsTask] = useState({});
  const [submittingTask, setSubmittingTask] = useState(false);
  const [submitTaskError, setSubmitTaskError] = useState("");

  useEffect(() => {
    const dashboardEl = document.getElementById("react-dashboard");
    if (dashboardEl)
      setCsrfToken(dashboardEl.getAttribute("data-csrf-token") || null);

    const dashboardScript = document.getElementById("dashboard-data");
    if (dashboardScript) {
      try {
        setDashboardData(JSON.parse(dashboardScript.textContent));
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

  const validateProject = (project) => {
    const newErrors = {};
    if (!project.name.trim()) newErrors.name = t("error_name_required");
    if (!project.description.trim())
      newErrors.description = t("error_description_required");
    return newErrors;
  };

  const handleSubmitProject = async () => {
    const v = validateProject(newProject);
    if (v.name || v.description) {
      setTouched({ name: true, description: true });
      setErrors(v);
      return;
    }
    setSubmitting(true);
    setSubmitError("");

    try {
      const response = await fetch("/api/project", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify(newProject),
      });
      if (!response.ok) throw new Error("Failed to create project");
      setShowProjectModal(false);
      setNewProject({ name: "", description: "" });
      setTouched({ name: false, description: false });
      setErrors({});
      window.location.reload();
    } catch (err) {
      setSubmitError(err.message);
    } finally {
      setSubmitting(false);
    }
  };

  const openEditProjectModal = (project) => {
    setEditProject({
      id: project.Id,
      name: project.Name ?? "",
      description: project.Description ?? "",
    });
    setEditTouched({ name: false, description: false });
    setEditErrors({});
    setSubmitEditError("");
    setShowEditProjectModal(true);
  };

  const validateEditProject = (project) => {
    const newErrors = {};
    if (!project.name.trim()) newErrors.name = t("error_name_required");
    if (!project.description.trim())
      newErrors.description = t("error_description_required");
    return newErrors;
  };

  const handleUpdateProject = async () => {
    const v = validateEditProject(editProject);
    if (v.name || v.description) {
      setEditTouched({ name: true, description: true });
      setEditErrors(v);
      return;
    }

    setSubmittingEdit(true);
    setSubmitEditError("");

    try {
      const res = await fetch(`/api/project/${editProject.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify({
          name: editProject.name,
          description: editProject.description,
        }),
      });

      if (!res.ok) {
        const text = await res.text().catch(() => null);
        throw new Error(text || "Failed to update project");
      }

      setShowEditProjectModal(false);
      setEditProject({ id: "", name: "", description: "" });
      setEditTouched({ name: false, description: false });
      setEditErrors({});
      window.location.reload();
    } catch (err) {
      setSubmitEditError(err.message || "Error");
    } finally {
      setSubmittingEdit(false);
    }
  };

  const validateTask = (task) => {
    const e = {};
    if (!task.title.trim()) e.title = t("error_title_required");
    return e;
  };

  const handleSubmitTask = async () => {
    const v = validateTask(newTask);
    if (v.title) {
      setTouchedTask({ title: true });
      setErrorsTask(v);
      return;
    }

    setSubmittingTask(true);
    setSubmitTaskError("");
    try {
      const res = await fetch("/api/taskitem", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify({
          ...newTask,
          projectId: newTask.projectId || null,
          dueDate: newTask.dueDate || null,
        }),
      });
      if (!res.ok) throw new Error("Failed to create task");
      setShowTaskModal(false);
      setNewTask({
        title: "",
        description: "",
        dueDate: "",
        projectId: "",
        isCompleted: false,
      });
      setTouchedTask({ title: false });
      setErrorsTask({});
      window.location.reload();
    } catch (err) {
      setSubmitTaskError(err.message || "Error");
    } finally {
      setSubmittingTask(false);
    }
  };

  return (
    <div style={styles.container}>
      <div style={styles.header}>
        <h2>
          {t("welcome")}, {username}
        </h2>
        <p>
          {t("projects")} : {projectCount} | {t("tasks")} : {taskCount}
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
          onClick={() => setShowTaskModal(true)}
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
                  {t("tasks")} : {p.TaskCount} | {t("completed")} :{" "}
                  {p.CompletedTasks}
                </p>
                <div style={styles.buttons}>
                  <button
                    style={{ ...styles.button, ...styles.btnPrimary }}
                    onClick={() => openEditProjectModal(p)}
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
                  {t("project")} : {task.ProjectName ?? t("n_a")}
                  <br />
                  {t("due")} : {task.DueDate ?? t("n_a")}
                  <br />
                  {t("completed")} : {task.IsCompleted ? "✅" : "❌"}
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
                  <button
                    style={{ ...styles.button, ...styles.btnPrimary }}
                    onClick={() =>
                      console.log("Add comment to task", task.Id, csrfToken)
                    }
                  >
                    {t("add_comment")}
                  </button>
                </div>
              </div>
            ))
          ) : (
            <p style={styles.empty}>{t("no_tasks")}</p>
          )}
        </div>
      </div>

      {/* Project Modal */}
      {showProjectModal && (
        <div style={styles.modalOverlay}>
          <div style={styles.modal}>
            <h3>{t("add_project")}</h3>
            <input
              type="text"
              placeholder={t("project_name")}
              value={newProject.name}
              onChange={(e) => {
                setNewProject({ ...newProject, name: e.target.value });
                setTouched({ ...touched, name: true });
              }}
              style={styles.modalInput}
            />
            {touched.name && errors.name && (
              <div style={styles.errorText}>{errors.name}</div>
            )}
            <textarea
              placeholder={t("project_description")}
              value={newProject.description}
              onChange={(e) => {
                setNewProject({ ...newProject, description: e.target.value });
                setTouched({ ...touched, description: true });
              }}
              style={styles.modalInput}
            />
            {touched.description && errors.description && (
              <div style={styles.errorText}>{errors.description}</div>
            )}
            {submitError && <div style={styles.errorText}>{submitError}</div>}
            <div style={styles.modalActions}>
              <button
                style={{ ...styles.button, ...styles.btnPrimary }}
                onClick={handleSubmitProject}
                disabled={submitting}
              >
                {t("ok")}
              </button>
              <button
                style={{ ...styles.button, ...styles.btnDanger }}
                onClick={() => setShowProjectModal(false)}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Edit Project Modal */}
      {showEditProjectModal && (
        <div style={styles.modalOverlay}>
          <div style={styles.modal}>
            <h3>
              {t("edit")}&nbsp;{t("project")}
            </h3>
            <input
              type="text"
              placeholder={t("project_name")}
              value={editProject.name}
              onChange={(e) => {
                setEditProject({ ...editProject, name: e.target.value });
                setEditTouched((prev) => ({ ...prev, name: true }));
              }}
              style={styles.modalInput}
            />
            {editTouched.name && editErrors.name && (
              <div style={styles.errorText}>{editErrors.name}</div>
            )}
            <textarea
              placeholder={t("project_description")}
              value={editProject.description}
              onChange={(e) => {
                setEditProject({ ...editProject, description: e.target.value });
                setEditTouched((prev) => ({ ...prev, description: true }));
              }}
              style={styles.modalInput}
            />
            {editTouched.description && editErrors.description && (
              <div style={styles.errorText}>{editErrors.description}</div>
            )}
            {submitEditError && (
              <div style={styles.errorText}>{submitEditError}</div>
            )}
            <div style={styles.modalActions}>
              <button
                style={{ ...styles.button, ...styles.btnPrimary }}
                onClick={handleUpdateProject}
                disabled={submittingEdit}
              >
                {t("ok")}
              </button>
              <button
                style={{ ...styles.button, ...styles.btnDanger }}
                onClick={() => setShowEditProjectModal(false)}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Task Modal */}
      {showTaskModal && (
        <div style={styles.modalOverlay}>
          <div style={styles.modal}>
            <h3>{t("add_task")}</h3>
            <input
              type="text"
              placeholder={t("task_title")}
              value={newTask.title}
              onChange={(e) => {
                setNewTask({ ...newTask, title: e.target.value });
                setTouchedTask({ ...touchedTask, title: true });
              }}
              style={styles.modalInput}
            />
            {touchedTask.title && errorsTask.title && (
              <div style={styles.errorText}>{errorsTask.title}</div>
            )}
            <textarea
              placeholder={t("task_description")}
              value={newTask.description}
              onChange={(e) =>
                setNewTask({ ...newTask, description: e.target.value })
              }
              style={styles.modalInput}
            />
            <input
              type="date"
              value={newTask.dueDate}
              onChange={(e) =>
                setNewTask({ ...newTask, dueDate: e.target.value })
              }
              style={styles.modalInput}
            />
            <select
              value={newTask.projectId}
              onChange={(e) =>
                setNewTask({ ...newTask, projectId: e.target.value })
              }
              style={styles.modalInput}
            >
              <option value="">{t("no_project")}</option>
              {projects.map((p) => (
                <option key={p.Id} value={p.Id}>
                  {p.Name}
                </option>
              ))}
            </select>
            <div
              style={{
                display: "flex",
                alignItems: "center",
                gap: "8px",
                marginTop: "8px",
              }}
            >
              <input
                id="isCompleted"
                type="checkbox"
                checked={newTask.isCompleted}
                onChange={(e) =>
                  setNewTask({ ...newTask, isCompleted: e.target.checked })
                }
              />
              <label htmlFor="isCompleted">{t("completed")}</label>
            </div>
            {submitTaskError && (
              <div style={styles.errorText}>{submitTaskError}</div>
            )}
            <div style={styles.modalActions}>
              <button
                style={{ ...styles.button, ...styles.btnPrimary }}
                onClick={handleSubmitTask}
                disabled={submittingTask}
              >
                {t("ok")}
              </button>
              <button
                style={{ ...styles.button, ...styles.btnDanger }}
                onClick={() => setShowTaskModal(false)}
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
