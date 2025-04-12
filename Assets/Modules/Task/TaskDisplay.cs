using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskDisplay : MonoBehaviour
{
    public GameObject taskUI; // 公开的GameObject，用于控制任务UI的显示和隐藏
    public Text taskText,taskText2; // 公开的Text组件，用于显示任务描述
    public GameObject tipUI; // 公开的GameObject，用于控制任务提示UI的显示和隐藏
    public Text tipText; // 公开的Text组件，用于显示任务提示描述
    public float displayDuration = 3f; // 任务UI显示的持续时间
    public Task firstIncompleteTask;//当前任务
    public int position,dizuo=0,chelun=0,tulun=0;//当前任务位置
    public pickupmethod pickupmethod1;
    public string[] utext = new string[4];
    private void Start()
    {
        utext[0] = "使用一次工具";
        utext[1] = "使用切割机切割出一个零件";
        utext[2] = "使用3D打印机打印出一个零件";
        utext[3] = "组装小车：底座(0/1)、车轮(0/1),凸轮(0/1)";
        TaskManager taskManager = TaskManager.Instance;
        firstIncompleteTask = taskManager.tasks[0];
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated += UpdateTaskDisplay; // 注册事件监听器
            //UpdateTaskDisplay(); // 初始化显示
        }
    }
    private void Update()
    {
        changetext();
        
    }
    private void OnDestroy()
    {
        TaskManager taskManager = TaskManager.Instance;
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated -= UpdateTaskDisplay; // 注销事件监听器
        }
    }

    private void UpdateTaskDisplay()//更新任务状态
    {
        TaskManager taskManager = TaskManager.Instance;
        if (taskManager != null)
        {
            // 查找第一个未完成的任务
            firstIncompleteTask = taskManager.tasks.Find(task => !task.isCompleted);
            position = taskManager.tasks.IndexOf(firstIncompleteTask);

            if (firstIncompleteTask != null)
            {
                // 显示任务描述
                taskText.text = firstIncompleteTask.description;
                tipText.text = firstIncompleteTask.description;
                taskText2.text = utext[position - 1];
                // 显示任务UI
                ShowTaskUI();
                ShowTipUI();
            }
            else
            {
                Debug.Log("mission");
                HideTaskUI();
                HideTipUI();
            }
        }
    }

    // 控制任务UI的显示
    public void ShowTaskUI()
    {
        if (taskUI != null)
        {
            taskUI.SetActive(true);
        }
    }
    // 控制任务UI的隐藏
    public void HideTaskUI()
    {
        if (taskUI!= null)
        {
            //taskUI.SetActive(false);
        }
    }

    // 控制提示UI的显示和隐藏
    public void ShowTipUI()
    {
        if (tipUI != null)
        {
            tipUI.SetActive(true);
            Invoke("HideTipUI", displayDuration);
        }
    }

    // 隐藏提示UI
    public void HideTipUI()
    {
        if (tipUI != null)
        {
            tipUI.SetActive(false);
        }
    }
    public void changetext()
    {
        TaskManager taskManager = TaskManager.Instance;
        if (firstIncompleteTask == taskManager.tasks[4]&&!(tulun==1&&chelun==1&&dizuo==1))
        {
            if (pickupmethod1.istulun)
            {
                tulun = 1;
            }
            if (pickupmethod1.ischelun)
            {
                chelun = 1;
            }
            if (pickupmethod1.isdizuo)
            {
                dizuo = 1;
            }
            taskText2.text = "组装小车：底座(" + dizuo.ToString() + "/1)、车轮(" + chelun.ToString() + "/1),凸轮(" + tulun.ToString() + "/1)";
        }
    }
}