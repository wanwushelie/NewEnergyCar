using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public event System.Action OnTaskUpdated; // 定义一个事件

    // // 初始化任务列表
    // void Start()
    // {
    //     tasks.Add(new Task("1", "前往普通车床"));
    //     tasks.Add(new Task("2", "前往激光切割机"));
    //     tasks.Add(new Task("3", "前往3D打印机"));
        
    //     // 打印任务初始化状态
    //     foreach (Task task in tasks)
    //     {
    //         Debug.Log($"Task ID: {task.id}, Description: {task.description}, IsCompleted: {task.isCompleted}");
    //     }
        
    // }

    // 完成任务
    public void CompleteTask(string taskId)
    {
        foreach (Task task in tasks)
        {
            if (task.id == taskId)
            {
                task.isCompleted = true;
                break;
            }
        }
        OnTaskUpdated?.Invoke(); // 触发事件
    }
}