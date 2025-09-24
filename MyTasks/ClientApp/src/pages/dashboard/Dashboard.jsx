import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import styles from "./Dashboard.module.css";

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
  const [touchedTask, setTouchedTask] = useState({
    title: false,
    dueDate: false,
  });
  const [errorsTask, setErrorsTask] = useState({});
  const [submittingTask, setSubmittingTask] = useState(false);
  const [submitTaskError, setSubmitTaskError] = useState("");

  const [showEditTaskModal, setShowEditTaskModal] = useState(false);
  const [editTask, setEditTask] = useState({
    id: "",
    title: "",
    description: "",
    dueDate: "",
    projectId: "",
    isCompleted: false,
  });
  const [editTouchedTask, setEditTouchedTask] = useState({
    title: false,
    description: false,
    dueDate: false,
  });
  const [editErrorsTask, setEditErrorsTask] = useState({});
  const [submittingEditTask, setSubmittingEditTask] = useState(false);
  const [submitEditTaskError, setSubmitEditTaskError] = useState("");

  const [showAddCommentModal, setShowAddCommentModal] = useState(false);
  const [commentTaskId, setCommentTaskId] = useState(null);
  const [newComment, setNewComment] = useState("");
  const [commentError, setCommentError] = useState("");
  const [submittingComment, setSubmittingComment] = useState(false);

  const [showDeleteProjectModal, setShowDeleteProjectModal] = useState(false);
  const [deleteProjectId, setDeleteProjectId] = useState(null);
  const [deletingProject, setDeletingProject] = useState(false);
  const [deleteError, setDeleteError] = useState("");

  const [showDeleteTaskModal, setShowDeleteTaskModal] = useState(false);
  const [deleteTaskId, setDeleteTaskId] = useState(null);
  const [deletingTask, setDeletingTask] = useState(false);
  const [deleteTaskError, setDeleteTaskError] = useState("");

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
      id: project.Id ?? "",
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

  const handleDeleteProject = async () => {
    if (!deleteProjectId) return;

    const id = deleteProjectId;
    setDeletingProject(true);
    setDeleteError("");

    try {
      const res = await fetch(`/api/project/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
      });

      if (!res.ok) {
        const text = await res.text().catch(() => null);
        throw new Error(text || "Failed to delete project");
      }

      setShowDeleteProjectModal(false);
      setDeleteProjectId(null);

      window.location.reload();
    } catch (err) {
      setDeleteError(err.message || "Error");
    } finally {
      setDeletingProject(false);
    }
  };

  const handleDeleteTask = async () => {
    if (!deleteTaskId) return;

    setDeletingTask(true);
    setDeleteTaskError("");

    try {
      const res = await fetch(`/api/taskitem/${deleteTaskId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
      });

      if (!res.ok) {
        const text = await res.text().catch(() => null);
        throw new Error(text || "Failed to delete task");
      }

      setShowDeleteTaskModal(false);
      setDeleteTaskId(null);

      window.location.reload();
    } catch (err) {
      setDeleteTaskError(err.message || "Error");
    } finally {
      setDeletingTask(false);
    }
  };

  const validateTask = (task) => {
    const e = {};
    if (!task.title.trim()) e.title = t("error_title_required");
    if (!task.description.trim())
      e.description = t("error_description_required");
    if (!task.dueDate) e.dueDate = t("error_due_date_required");
    if (!task.projectId) e.projectId = t("error_project_required");
    return e;
  };

  const validateEditTask = (task) => {
    const errors = {};
    if (!task.title.trim()) errors.title = t("error_title_required");
    if (!task.description.trim())
      errors.description = t("error_description_required");
    if (!task.dueDate) errors.dueDate = t("error_due_date_required");
    return errors;
  };

  const handleSubmitTask = async () => {
    const v = validateTask(newTask);
    setTouchedTask({
      title: true,
      description: true,
      dueDate: true,
      projectId: true,
    });
    setErrorsTask(v);
    if (Object.keys(v).length > 0) return;

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
      setTouchedTask({
        title: false,
        dueDate: false,
        description: false,
        projectId: false,
      });
      setErrorsTask({});
      window.location.reload();
    } catch (err) {
      setSubmitTaskError(err.message || "Error");
    } finally {
      setSubmittingTask(false);
    }
  };

  const openEditTaskModal = (task) => {
    setEditTask({
      id: task.Id ?? "",
      title: task.Title ?? "",
      description: task.Description ?? "",
      dueDate: task.DueDate ?? "",
      projectId: task.ProjectId ?? "",
      isCompleted: task.IsCompleted ?? false,
    });
    setEditTouchedTask({ title: false, description: false, dueDate: false });
    setEditErrorsTask({});
    setSubmitEditTaskError("");
    setShowEditTaskModal(true);
  };

  const handleUpdateTask = async () => {
    const errors = validateEditTask(editTask);
    if (errors.title || errors.description || errors.dueDate) {
      setEditTouchedTask({ title: true, description: true, dueDate: true });
      setEditErrorsTask(errors);
      return;
    }

    setSubmittingEditTask(true);
    setSubmitEditTaskError("");

    try {
      const res = await fetch(`/api/taskitem/${editTask.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify(editTask),
      });
      if (!res.ok) throw new Error("Failed to update task");

      setShowEditTaskModal(false);
      setEditTask({
        id: "",
        title: "",
        description: "",
        dueDate: "",
        projectId: "",
        isCompleted: false,
      });
      setEditTouchedTask({ title: false, description: false, dueDate: false });
      setEditErrorsTask({});
      window.location.reload();
    } catch (err) {
      setSubmitEditTaskError(err.message || "Error");
    } finally {
      setSubmittingEditTask(false);
    }
  };

  const handleSubmitComment = async () => {
    if (!newComment.trim()) {
      setCommentError(t("error_content_required"));
      return;
    }
    setSubmittingComment(true);
    setCommentError("");
    try {
      const res = await fetch("/api/taskcomment", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify({
          content: newComment,
          createdAt: new Date().toISOString(),
          taskItemId: commentTaskId,
        }),
      });
      if (!res.ok) throw new Error("Failed to create comment");
      setShowAddCommentModal(false);
      setNewComment("");
      setCommentTaskId(null);
      window.location.reload();
    } catch (err) {
      setCommentError(err.message || "Error");
    } finally {
      setSubmittingComment(false);
    }
  };

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h2>
          {t("welcome")}, {username}
        </h2>
        <p>
          {t("projects")} : {projectCount} | {t("tasks")} : {taskCount}
        </p>
      </div>

      <div className={styles.buttons}>
        <button
          className="btn btn-primary"
          onClick={() => setShowProjectModal(true)}
        >
          ➕ {t("add_project")}
        </button>
        <button
          className="btn btn-primary"
          onClick={() => setShowTaskModal(true)}
        >
          ➕ {t("add_task")}
        </button>
      </div>

      {/* Projekty */}
      <div className={styles.section}>
        <h3>{t("projects")}</h3>
        <div className={styles.list}>
          {projects.length > 0 ? (
            projects.map((p) => (
              <div key={p.Id} className={styles.card}>
                <div className={styles.cardTitle}>{p.Name}</div>
                <p className={styles.cardText}>{p.Description}</p>
                <p className={styles.cardText}>
                  {t("tasks")} : {p.TaskCount ?? 0} | {t("completed")} :{" "}
                  {p.CompletedTasks ?? 0}
                </p>
                <div className={styles.buttons}>
                  <button
                    className="btn btn-primary"
                    onClick={() => openEditProjectModal(p)}
                  >
                    {t("edit")}
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() => {
                      setDeleteProjectId(p.Id);
                      setShowDeleteProjectModal(true);
                    }}
                  >
                    {t("delete")}
                  </button>
                </div>
              </div>
            ))
          ) : (
            <p className={styles.empty}>{t("no_projects")}</p>
          )}
        </div>
      </div>

      {/* Zadania */}
      <div className={styles.section}>
        <h3>{t("tasks")}</h3>
        <div className={styles.list}>
          {tasks.length > 0 ? (
            tasks.map((task) => (
              <div key={task.Id} className={styles.card}>
                <div className={styles.cardTitle}>{task.Title}</div>
                <p className={styles.cardText}>{task.Description}</p>
                <p className={styles.cardText}>
                  {t("project")} : {task.ProjectName ?? t("n_a")}
                  <br />
                  {t("due")} : {task.DueDate ?? t("n_a")}
                  <br />
                  {t("completed")} : {task.IsCompleted ? "✅" : "❌"}
                </p>
                {task.LastComment && (
                  <p className={styles.cardText}>
                    💬 {task.LastComment} ({task.LastCommentAt ?? t("n_a")})
                  </p>
                )}
                <div className={styles.buttons}>
                  <button
                    className="btn btn-primary"
                    onClick={() => openEditTaskModal(task)}
                  >
                    {t("edit")}
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() => {
                      setDeleteTaskId(task.Id);
                      setShowDeleteTaskModal(true);
                    }}
                  >
                    {t("delete")}
                  </button>
                  <button
                    className="btn btn-primary"
                    onClick={() => {
                      setCommentTaskId(task.Id);
                      setShowAddCommentModal(true);
                    }}
                  >
                    {t("add_comment")}
                  </button>
                </div>
              </div>
            ))
          ) : (
            <p className={styles.empty}>{t("no_tasks")}</p>
          )}
        </div>
      </div>

      {/* Modal dodawania projektu */}
      {showProjectModal && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>{t("add_project")}</h3>
            <input
              type="text"
              placeholder={t("project_name")}
              value={newProject.name}
              onChange={(e) => {
                setNewProject({ ...newProject, name: e.target.value });
                setTouched((prev) => ({ ...prev, name: true }));
                setErrors((prev) => ({ ...prev, name: "" }));
              }}
              className={styles.modalInput}
            />
            {errors.name && (
              <div className={styles.errorText}>{errors.name}</div>
            )}
            <textarea
              placeholder={t("project_description")}
              value={newProject.description}
              onChange={(e) => {
                setNewProject({ ...newProject, description: e.target.value });
                setTouched((prev) => ({ ...prev, description: true }));
                setErrors((prev) => ({ ...prev, description: "" }));
              }}
              className={styles.modalInput}
            />
            {errors.description && (
              <div className={styles.errorText}>{errors.description}</div>
            )}
            {submitError && (
              <div className={styles.errorText}>{submitError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={handleSubmitProject}
                disabled={submitting}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() => setShowProjectModal(false)}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Modal edycji projektu */}
      {showEditProjectModal && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>
              {t("edit")} {t("project")}
            </h3>
            <input
              type="text"
              placeholder={t("project_name")}
              value={editProject.name}
              onChange={(e) => {
                setEditProject({ ...editProject, name: e.target.value });
                setEditTouched((prev) => ({ ...prev, name: true }));
                setEditErrors((prev) => ({ ...prev, name: "" }));
              }}
              className={styles.modalInput}
            />
            {editErrors.name && (
              <div className={styles.errorText}>{editErrors.name}</div>
            )}
            <textarea
              placeholder={t("project_description")}
              value={editProject.description}
              onChange={(e) => {
                setEditProject({ ...editProject, description: e.target.value });
                setEditTouched((prev) => ({ ...prev, description: true }));
                setEditErrors((prev) => ({ ...prev, description: "" }));
              }}
              className={styles.modalInput}
            />
            {editErrors.description && (
              <div className={styles.errorText}>{editErrors.description}</div>
            )}
            {submitEditError && (
              <div className={styles.errorText}>{submitEditError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={handleUpdateProject}
                disabled={submittingEdit}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() => setShowEditProjectModal(false)}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Modal usuwania projektu */}
      {showDeleteProjectModal && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>
              {t("delete")} {t("project")}
            </h3>
            <p>{t("deleteProjectConfirmation")}</p>
            {deleteError && (
              <div className={styles.errorText}>{deleteError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-danger"
                onClick={handleDeleteProject}
                disabled={deletingProject}
              >
                {t("delete")}
              </button>
              <button
                className="btn btn-primary"
                onClick={() => {
                  setShowDeleteProjectModal(false);
                  setDeleteProjectId(null);
                  setDeleteError("");
                }}
                disabled={deletingProject}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Modal dodawania zadania */}
      {showTaskModal && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>{t("add_task")}</h3>
            <input
              type="text"
              placeholder={t("task_title")}
              value={newTask.title}
              onChange={(e) => {
                setNewTask({ ...newTask, title: e.target.value });
                setTouchedTask((prev) => ({ ...prev, title: true }));
                setErrorsTask((prev) => ({ ...prev, title: "" }));
              }}
              className={styles.modalInput}
            />
            {touchedTask.title && errorsTask.title && (
              <div className={styles.errorText}>{errorsTask.title}</div>
            )}
            <textarea
              placeholder={t("task_description")}
              value={newTask.description}
              onChange={(e) => {
                setNewTask({ ...newTask, description: e.target.value });
                setTouchedTask((prev) => ({ ...prev, description: true }));
                setErrorsTask((prev) => ({ ...prev, description: "" }));
              }}
              className={styles.modalInput}
            />
            {touchedTask.description && errorsTask.description && (
              <div className={styles.errorText}>{errorsTask.description}</div>
            )}
            <input
              type="date"
              value={newTask.dueDate.split("T")[0] ?? ""}
              onChange={(e) => {
                setNewTask({ ...newTask, dueDate: e.target.value });
                setTouchedTask((prev) => ({ ...prev, dueDate: true }));
                setErrorsTask((prev) => ({ ...prev, dueDate: "" }));
              }}
              className={styles.modalInput}
            />
            {touchedTask.dueDate && errorsTask.dueDate && (
              <div className={styles.errorText}>{errorsTask.dueDate}</div>
            )}
            <select
              value={newTask.projectId}
              onChange={(e) => {
                setNewTask({ ...newTask, projectId: e.target.value });
                setTouchedTask((prev) => ({ ...prev, projectId: true }));
                setErrorsTask((prev) => ({ ...prev, projectId: "" }));
              }}
              className={styles.modalInput}
            >
              <option value="">{t("select_project")}</option>
              {projects.map((p) => (
                <option key={p.Id} value={p.Id}>
                  {p.Name}
                </option>
              ))}
            </select>
            {touchedTask.projectId && errorsTask.projectId && (
              <div className={styles.errorText}>{errorsTask.projectId}</div>
            )}
            {submitTaskError && (
              <div className={styles.errorText}>{submitTaskError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={handleSubmitTask}
                disabled={submittingTask}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() => setShowTaskModal(false)}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Modal edycji zadania */}
      {showEditTaskModal && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>
              {t("edit")} {t("task")}
            </h3>
            <input
              type="text"
              placeholder={t("task_title")}
              value={editTask.title}
              onChange={(e) => {
                setEditTask({ ...editTask, title: e.target.value });
                setEditTouchedTask((prev) => ({ ...prev, title: true }));
              }}
              className={styles.modalInput}
            />
            {editTouchedTask.title && editErrorsTask.title && (
              <div className={styles.errorText}>{editErrorsTask.title}</div>
            )}
            <textarea
              placeholder={t("task_description")}
              value={editTask.description}
              onChange={(e) =>
                setEditTask({ ...editTask, description: e.target.value })
              }
              className={styles.modalInput}
            />
            {editTouchedTask.description && editErrorsTask.description && (
              <div className={styles.errorText}>
                {editErrorsTask.description}
              </div>
            )}
            <input
              type="date"
              value={editTask.dueDate.split("T")[0] ?? ""}
              onChange={(e) => {
                setEditTask({ ...editTask, dueDate: e.target.value });
                setEditTouchedTask((prev) => ({ ...prev, dueDate: true }));
              }}
              className={styles.modalInput}
            />
            {editTouchedTask.dueDate && editErrorsTask.dueDate && (
              <div className={styles.errorText}>{editErrorsTask.dueDate}</div>
            )}
            <label>
              <input
                type="checkbox"
                checked={editTask.isCompleted}
                onChange={(e) =>
                  setEditTask({ ...editTask, isCompleted: e.target.checked })
                }
              />{" "}
              {t("completed")}
            </label>
            {submitEditTaskError && (
              <div className={styles.errorText}>{submitEditTaskError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={handleUpdateTask}
                disabled={submittingEditTask}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() => setShowEditTaskModal(false)}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Modal dodawania komentarza */}
      {showAddCommentModal && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>{t("add_comment")}</h3>
            <textarea
              placeholder={t("comment_content")}
              value={newComment}
              onChange={(e) => setNewComment(e.target.value)}
              className={styles.modalInput}
            />
            {commentError && (
              <div className={styles.errorText}>{commentError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={handleSubmitComment}
                disabled={submittingComment}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() => setShowAddCommentModal(false)}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Modal usuwania zadania */}
      {showDeleteTaskModal && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>
              {t("delete")} {t("task")}
            </h3>
            <p>{t("deleteTaskConfirmation")}</p>
            {deleteTaskError && (
              <div className={styles.errorText}>{deleteTaskError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-danger"
                onClick={handleDeleteTask}
                disabled={deletingTask}
              >
                {t("delete")}
              </button>
              <button
                className="btn btn-primary"
                onClick={() => {
                  setShowDeleteTaskModal(false);
                  setDeleteTaskId(null);
                  setDeleteTaskError("");
                }}
                disabled={deletingTask}
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
