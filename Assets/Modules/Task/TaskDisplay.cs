using UnityEngine;
using UnityEngine.UI;

public class TaskDisplay : MonoBehaviour
{
    public GameObject taskUI; // 公开的GameObject，用于控制任务UI的显示和隐藏
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
                // 显示任务UI
                ShowTaskUI(true);
            }
            else
            {
                // 如果所有任务都已完成，隐藏任务UI
                ShowTaskUI(false);
            }
        }
    }

    // 控制任务UI的显示和隐藏
    private void ShowTaskUI(bool show)
    {
        if (taskUI != null)
        {
            taskUI.SetActive(show);
        }
    }
}