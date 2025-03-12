using UnityEngine;

public class CompleteTaskKey : MonoBehaviour
{
    public string taskIdToComplete = "1"; // 要完成的任务ID，默认为任务一

    private void Update()
    {
        // 检测是否按下了M键
        if (Input.GetKeyDown(KeyCode.M))
        {
            CompleteTask();
        }
    }

    private void CompleteTask()
    {
        // 找到场景中的TaskManager实例
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            // 调用TaskManager的CompleteTask方法完成指定任务
            taskManager.CompleteTask(taskIdToComplete);
        }
        else
        {
            Debug.LogError("TaskManager not found in the scene!");
        }
    }
}