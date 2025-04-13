using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    public List<Task> tasks = new List<Task>();
    public event System.Action OnTaskUpdated;

    // 添加 TaskDisplay 引用
    public TaskDisplay taskDisplay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void CompleteTask(string taskId)
    {
        foreach (Task task in tasks)
        {
            if (task.id == taskId)
            {
                task.isCompleted = true;
                //taskDisplay.taskText.text +="(已完成)".ToString();
                break;
                //打印数据
                Debug.Log("任务已完成：" + task.id + " " + task.description + " " + task.isCompleted);
            }
        }
        // 隐藏任务栏 UI
        if (taskDisplay!= null)
        {
            taskDisplay.HideTaskUI();
            //打印
            Debug.Log("任务栏UI已隐藏");
        }
    }
  
    public void UpdateTaskUI()
    {
        // 更新任务栏 UI
        OnTaskUpdated?.Invoke();
    }

    public void HideTaskSystemUI()
    {
        if (taskDisplay != null)
        {
            taskDisplay.HideTaskUI();
            taskDisplay.HideTipUI();
        }
    }
}