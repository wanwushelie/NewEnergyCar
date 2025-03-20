using UnityEngine;
using UnityEngine.UI;

public class TaskDisplay : MonoBehaviour
{
    public GameObject taskUI; // 公开的GameObject，用于控制任务UI的显示和隐藏
    public Text taskText; // 公开的Text组件，用于显示任务描述
    public GameObject tipUI; // 公开的GameObject，用于控制任务提示UI的显示和隐藏
    public Text tipText; // 公开的Text组件，用于显示任务提示描述
    public float displayDuration = 3f; // 任务UI显示的持续时间

    private void Start()
    {
        TaskManager taskManager = TaskManager.Instance;
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated += UpdateTaskDisplay; // 注册事件监听器
            //UpdateTaskDisplay(); // 初始化显示
        }
    }

    private void OnDestroy()
    {
        TaskManager taskManager = TaskManager.Instance;
        if (taskManager != null)
        {
            taskManager.OnTaskUpdated -= UpdateTaskDisplay; // 注销事件监听器
        }
    }

    private void UpdateTaskDisplay()
    {
        TaskManager taskManager = TaskManager.Instance;
        if (taskManager != null)
        {
            // 查找第一个未完成的任务
            Task firstIncompleteTask = taskManager.tasks.Find(task => !task.isCompleted);

            if (firstIncompleteTask != null)
            {
                // 显示任务描述
                taskText.text = firstIncompleteTask.description;
                tipText.text = firstIncompleteTask.description;
                // 显示任务UI
                ShowTaskUI();
                ShowTipUI();
            }
            else
            {
                //   如果所有任务都已完成，隐藏任务UI
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
            taskUI.SetActive(false);
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
}