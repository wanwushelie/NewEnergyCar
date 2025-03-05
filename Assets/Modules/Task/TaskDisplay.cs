using UnityEngine;
using UnityEngine.UI;

public class TaskDisplay : MonoBehaviour
{
    public Text taskTextTemplate; // 任务文本模板
    public RectTransform taskContent; // 任务内容区域

    private void Start()
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated += UpdateTasks; // 注册事件监听器
            taskManager.UpdateTaskDisplay(); // 初始化显示
        }
    }

    private void OnDestroy()
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated -= UpdateTasks; // 注销事件监听器
        }
    }

    private void UpdateTasks()
    {
        foreach (Transform child in taskContent)
        {
            Destroy(child.gameObject); // 清除旧的任务项
        }

        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            foreach (Task task in taskManager.tasks)
            {
                // 创建新的任务项
                GameObject taskObject = Instantiate(taskTextTemplate.gameObject, taskContent);
                taskObject.transform.GetComponent<Text>().text = task.id + ": " + task.description + " - " + (task.isCompleted ? "已完成" : "未完成");
                taskObject.transform.GetComponent<Text>().color = task.isCompleted ? Color.gray : Color.black;
            }
        }
    }
}