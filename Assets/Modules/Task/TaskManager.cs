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
}