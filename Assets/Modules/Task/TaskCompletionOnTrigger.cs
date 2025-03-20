using UnityEngine;

public class TaskCompletionOnTrigger : MonoBehaviour
{
    // 用于指定要完成的任务 ID
    public string targetTaskId;
    // 标志位，用于判断任务是否已经完成
    private bool taskCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是玩家进入触发器，并且任务还未完成
        if (other.CompareTag("Player") && !taskCompleted)
        {
            CompleteTask();
        }
    }

    private void CompleteTask()
    {
        TaskManager taskManager = TaskManager.Instance;
        if (taskManager != null && !string.IsNullOrEmpty(targetTaskId))
        {
            // 调用 TaskManager 的 CompleteTask 方法完成任务
            taskManager.CompleteTask(targetTaskId);
            Debug.Log($"任务 {targetTaskId} 已完成");
            // 设置任务完成标志位为 true
            taskCompleted = true;
            // 禁用脚本，使其不再发挥作用
            this.enabled = false;
        }
    }
}