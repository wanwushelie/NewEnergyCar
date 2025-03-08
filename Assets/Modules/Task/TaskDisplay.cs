using UnityEngine;
using UnityEngine.UI;

public class TaskDisplay : MonoBehaviour
{
    public Text taskText; // 公开的Text组件，用于显示任务描述

    private void Start()
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated += UpdateTaskDisplay; // 注册事件监听器
            UpdateTaskDisplay(); // 初始化显示
        }
    }

    private void OnDestroy()
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated -= UpdateTaskDisplay; // 注销事件监听器
        }
    }

    private void UpdateTaskDisplay()
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            // 查找第一个未完成的任务
            Task firstIncompleteTask = taskManager.tasks.Find(task => !task.isCompleted);

            if (firstIncompleteTask != null)
            {
                // 显示任务描述
                taskText.text = firstIncompleteTask.description;
            }
            else
            {
                // 如果所有任务都已完成，显示提示信息
                taskText.text = "所有任务已完成！";
            }
        }
    }
}