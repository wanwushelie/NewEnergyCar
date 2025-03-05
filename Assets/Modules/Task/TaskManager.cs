using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public event System.Action OnTaskUpdated; // 定义一个事件

    // 初始化任务列表
    void Start()
    {
        tasks.Add(new Task("1", "收集10个苹果"));
        tasks.Add(new Task("2", "找到隐藏的宝藏"));
        tasks.Add(new Task("3", "击败邪恶的巨龙"));
    }

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

    // 更新任务显示
    public void UpdateTaskDisplay()
    {
        foreach (Task task in tasks)
        {
            // 这里可以调用 UI 更新逻辑
            Debug.Log(task.id + ": " + task.description + " - " + (task.isCompleted ? "已完成" : "未完成"));
        }
    }
}