using UnityEngine;

public class TaskButton : MonoBehaviour
{
    public string taskId; // 任务ID，用于标识哪个任务
    public TaskManager taskManager; // 任务管理器的引用

    void Start()
    {
        // 如果在Inspector中没有手动设置taskManager，则自动查找
        if (taskManager == null)
        {
            taskManager = FindObjectOfType<TaskManager>();
        }
    }

    public void OnButtonClick()
    {
        if (taskManager != null)
        {
            taskManager.CompleteTask(taskId);
        }
        else
        {
            Debug.LogError("TaskManager not found!");
        }
    }
}