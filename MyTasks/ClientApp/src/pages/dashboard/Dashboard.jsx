import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import styles from "./Dashboard.module.css";

export default function Dashboard() {
  const { t } = useTranslation();
  const [dashboardData, setDashboardData] = useState({});
  const [csrfToken, setCsrfToken] = useState(null);

  const [modals, setModals] = useState({
    project: {
      show: false,
      data: { id: "", name: "", description: "" },
      touched: { name: false, description: false },
      errors: {},
      submitting: false,
      submitError: "",
    },
    task: {
      show: false,
      data: {
        id: "",
        title: "",
        description: "",
        dueDate: "",
        projectId: "",
        isCompleted: false,
      },
      touched: {
        title: false,
        description: false,
        dueDate: false,
        projectId: false,
      },
      errors: {},
      submitting: false,
      submitError: "",
    },
    comment: {
      show: false,
      data: { taskId: null, content: "" },
      errors: {},
      submitting: false,
      submitError: "",
    },
    delete: {
      projectId: null,
      taskId: null,
      deleting: false,
      deleteError: "",
    },
  });

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
    const errors = {};
    if (!project.name.trim()) errors.name = t("error_name_required");
    if (!project.description.trim())
      errors.description = t("error_description_required");
    return errors;
  };

  const validateTask = (task) => {
    const errors = {};
    if (!task.title.trim()) errors.title = t("error_title_required");
    if (!task.description.trim())
      errors.description = t("error_description_required");
    if (!task.dueDate) errors.dueDate = t("error_due_date_required");
    if (!task.projectId) errors.projectId = t("error_project_required");
    return errors;
  };

  const handleSubmit = async ({ type, method, url, data, validate }) => {
    const errors = validate(data);
    if (Object.keys(errors).length > 0) {
      setModals((prev) => ({
        ...prev,
        [type]: {
          ...prev[type],
          errors,
          touched: Object.fromEntries(
            Object.keys(errors).map((k) => [k, true])
          ),
        },
      }));
      return;
    }

    setModals((prev) => ({
      ...prev,
      [type]: { ...prev[type], submitting: true, submitError: "" },
    }));

    try {
      const res = await fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
        body: JSON.stringify(data),
      });
      if (!res.ok) {
        const text = await res.text().catch(() => null);
        throw new Error(
          text || `Failed to ${method === "POST" ? "create" : "update"} ${type}`
        );
      }
      setModals((prev) => ({
        ...prev,
        [type]: {
          ...prev[type],
          show: false,
          data: Object.keys(prev[type].data).reduce(
            (acc, k) => ({ ...acc, [k]: "" }),
            {}
          ),
          touched: Object.keys(prev[type].touched).reduce(
            (acc, k) => ({ ...acc, [k]: false }),
            {}
          ),
          errors: {},
          submitting: false,
          submitError: "",
        },
      }));
      window.location.reload();
    } catch (err) {
      setModals((prev) => ({
        ...prev,
        [type]: {
          ...prev[type],
          submitting: false,
          submitError: err.message || "Error",
        },
      }));
    }
  };

  const handleDelete = async ({ type, id }) => {
    if (!id) return;
    setModals((prev) => ({
      ...prev,
      delete: { ...prev.delete, deleting: true, deleteError: "" },
    }));

    try {
      const url =
        type === "project" ? `/api/project/${id}` : `/api/taskitem/${id}`;
      const res = await fetch(url, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          "X-CSRF-TOKEN": csrfToken,
        },
        credentials: "include",
      });
      if (!res.ok) {
        const text = await res.text().catch(() => null);
        throw new Error(text || `Failed to delete ${type}`);
      }
      setModals((prev) => ({
        ...prev,
        delete: {
          ...prev.delete,
          deleting: false,
          projectId: null,
          taskId: null,
          deleteError: "",
        },
        [`show${type.charAt(0).toUpperCase() + type.slice(1)}Modal`]: false,
      }));
      window.location.reload();
    } catch (err) {
      setModals((prev) => ({
        ...prev,
        delete: {
          ...prev.delete,
          deleting: false,
          deleteError: err.message || "Error",
        },
      }));
    }
  };

  const openEditModal = (type, data) => {
    const modalData =
      type === "project"
        ? { id: data.Id, name: data.Name, description: data.Description }
        : {
            id: data.Id,
            title: data.Title,
            description: data.Description,
            dueDate: data.DueDate ?? "",
            projectId: data.ProjectId ?? "",
            isCompleted: data.IsCompleted ?? false,
          };
    setModals((prev) => ({
      ...prev,
      [type]: {
        ...prev[type],
        show: true,
        data: modalData,
        touched: Object.keys(prev[type].touched).reduce(
          (acc, k) => ({ ...acc, [k]: false }),
          {}
        ),
        errors: {},
        submitError: "",
      },
    }));
  };

  const handleInputChange = (type, field, value) => {
    setModals((prev) => ({
      ...prev,
      [type]: {
        ...prev[type],
        data: { ...prev[type].data, [field]: value },
        touched: { ...prev[type].touched, [field]: true },
        errors: { ...prev[type].errors, [field]: "" },
      },
    }));
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
          onClick={() =>
            setModals((prev) => ({
              ...prev,
              project: { ...prev.project, show: true },
            }))
          }
        >
          ➕ {t("add_project")}
        </button>
        <button
          className="btn btn-primary"
          onClick={() =>
            setModals((prev) => ({
              ...prev,
              task: { ...prev.task, show: true },
            }))
          }
        >
          ➕ {t("add_task")}
        </button>
      </div>

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
                    onClick={() => openEditModal("project", p)}
                  >
                    {t("edit")}
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() =>
                      setModals((prev) => ({
                        ...prev,
                        delete: { ...prev.delete, projectId: p.Id },
                        showDeleteProjectModal: true,
                      }))
                    }
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
                    onClick={() => openEditModal("task", task)}
                  >
                    {t("edit")}
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() =>
                      setModals((prev) => ({
                        ...prev,
                        delete: { ...prev.delete, taskId: task.Id },
                        showDeleteTaskModal: true,
                      }))
                    }
                  >
                    {t("delete")}
                  </button>
                  <button
                    className="btn btn-primary"
                    onClick={() =>
                      setModals((prev) => ({
                        ...prev,
                        comment: {
                          ...prev.comment,
                          show: true,
                          data: { taskId: task.Id, content: "" },
                        },
                      }))
                    }
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

      {modals.project.show && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>
              {modals.project.data.id
                ? `${t("edit")} ${t("project")}`
                : t("add_project")}
            </h3>
            <input
              type="text"
              placeholder={t("project_name")}
              value={modals.project.data.name}
              onChange={(e) =>
                handleInputChange("project", "name", e.target.value)
              }
              className={styles.modalInput}
            />
            {modals.project.touched.name && modals.project.errors.name && (
              <div className={styles.errorText}>
                {modals.project.errors.name}
              </div>
            )}
            <textarea
              placeholder={t("project_description")}
              value={modals.project.data.description}
              onChange={(e) =>
                handleInputChange("project", "description", e.target.value)
              }
              className={styles.modalInput}
            />
            {modals.project.touched.description &&
              modals.project.errors.description && (
                <div className={styles.errorText}>
                  {modals.project.errors.description}
                </div>
              )}
            {modals.project.submitError && (
              <div className={styles.errorText}>
                {modals.project.submitError}
              </div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={() =>
                  handleSubmit({
                    type: "project",
                    method: modals.project.data.id ? "PUT" : "POST",
                    url: modals.project.data.id
                      ? `/api/project/${modals.project.data.id}`
                      : "/api/project",
                    data: {
                      name: modals.project.data.name,
                      description: modals.project.data.description,
                    },
                    validate: validateProject,
                  })
                }
                disabled={modals.project.submitting}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() =>
                  setModals((prev) => ({
                    ...prev,
                    project: { ...prev.project, show: false, submitError: "" },
                  }))
                }
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {modals.task.show && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>
              {modals.task.data.id
                ? `${t("edit")} ${t("task")}`
                : t("add_task")}
            </h3>
            <input
              type="text"
              placeholder={t("task_title")}
              value={modals.task.data.title}
              onChange={(e) =>
                handleInputChange("task", "title", e.target.value)
              }
              className={styles.modalInput}
            />
            {modals.task.touched.title && modals.task.errors.title && (
              <div className={styles.errorText}>{modals.task.errors.title}</div>
            )}
            <textarea
              placeholder={t("task_description")}
              value={modals.task.data.description}
              onChange={(e) =>
                handleInputChange("task", "description", e.target.value)
              }
              className={styles.modalInput}
            />
            {modals.task.touched.description &&
              modals.task.errors.description && (
                <div className={styles.errorText}>
                  {modals.task.errors.description}
                </div>
              )}
            <input
              type="date"
              value={modals.task.data.dueDate.split("T")[0] ?? ""}
              onChange={(e) =>
                handleInputChange("task", "dueDate", e.target.value)
              }
              className={styles.modalInput}
            />
            {modals.task.touched.dueDate && modals.task.errors.dueDate && (
              <div className={styles.errorText}>
                {modals.task.errors.dueDate}
              </div>
            )}
            <select
              value={modals.task.data.projectId}
              onChange={(e) =>
                handleInputChange("task", "projectId", e.target.value)
              }
              className={styles.modalInput}
            >
              <option value="">{t("select_project")}</option>
              {projects.map((p) => (
                <option key={p.Id} value={p.Id}>
                  {p.Name}
                </option>
              ))}
            </select>
            {modals.task.touched.projectId && modals.task.errors.projectId && (
              <div className={styles.errorText}>
                {modals.task.errors.projectId}
              </div>
            )}
            <label>
              <input
                type="checkbox"
                checked={modals.task.data.isCompleted}
                onChange={(e) =>
                  handleInputChange("task", "isCompleted", e.target.checked)
                }
              />{" "}
              {t("completed")}
            </label>
            {modals.task.submitError && (
              <div className={styles.errorText}>{modals.task.submitError}</div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={() =>
                  handleSubmit({
                    type: "task",
                    method: modals.task.data.id ? "PUT" : "POST",
                    url: modals.task.data.id
                      ? `/api/taskitem/${modals.task.data.id}`
                      : "/api/taskitem",
                    data: modals.task.data,
                    validate: validateTask,
                  })
                }
                disabled={modals.task.submitting}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() =>
                  setModals((prev) => ({
                    ...prev,
                    task: { ...prev.task, show: false, submitError: "" },
                  }))
                }
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {modals.comment.show && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>{t("add_comment")}</h3>
            <textarea
              placeholder={t("comment_content")}
              value={modals.comment.data.content}
              onChange={(e) =>
                handleInputChange("comment", "content", e.target.value)
              }
              className={styles.modalInput}
            />
            {modals.comment.submitError && (
              <div className={styles.errorText}>
                {modals.comment.submitError}
              </div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-primary"
                onClick={() =>
                  handleSubmit({
                    type: "comment",
                    method: "POST",
                    url: "/api/taskcomment",
                    data: {
                      content: modals.comment.data.content,
                      taskItemId: modals.comment.data.taskId,
                      createdAt: new Date().toISOString(),
                    },
                    validate: (d) =>
                      !d.content.trim()
                        ? { content: t("error_content_required") }
                        : {},
                  })
                }
                disabled={modals.comment.submitting}
              >
                {t("ok")}
              </button>
              <button
                className="btn btn-danger"
                onClick={() =>
                  setModals((prev) => ({
                    ...prev,
                    comment: { ...prev.comment, show: false, submitError: "" },
                  }))
                }
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}

      {(modals.delete.projectId || modals.delete.taskId) && (
        <div className={styles.modalOverlay}>
          <div className={styles.modal}>
            <h3>{t("delete")}</h3>
            <p>
              {modals.delete.projectId
                ? t("deleteProjectConfirmation")
                : t("deleteTaskConfirmation")}
            </p>
            {modals.delete.deleteError && (
              <div className={styles.errorText}>
                {modals.delete.deleteError}
              </div>
            )}
            <div className={styles.modalActions}>
              <button
                className="btn btn-danger"
                onClick={() =>
                  handleDelete({
                    type: modals.delete.projectId ? "project" : "task",
                    id: modals.delete.projectId ?? modals.delete.taskId,
                  })
                }
                disabled={modals.delete.deleting}
              >
                {t("delete")}
              </button>
              <button
                className="btn btn-primary"
                onClick={() =>
                  setModals((prev) => ({
                    ...prev,
                    delete: {
                      ...prev.delete,
                      projectId: null,
                      taskId: null,
                      deleteError: "",
                      deleting: false,
                    },
                  }))
                }
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
