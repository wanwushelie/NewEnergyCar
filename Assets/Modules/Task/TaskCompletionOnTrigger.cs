using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TaskCompletionOnTrigger : MonoBehaviour
{
    // 用于指定要完成的任务 ID
    public string targetTaskId;
    // 标志位，用于判断任务是否已经完成
    private bool taskCompleted = false;
    public TaskDisplay taskDisplay;
    public bool utask,havefinished,firsttime=false;
    public printdata printdata1;
    public PolygonDrawer polygonDrawer1;
    public lingjianused lingjianused1;
    public Assemble assemble1;
    public List<Sprite> usprite;
    public FloatingWindow floatingWindow;
    public Task uniqueTask;
    public TaskManager taskManager;

    private void Start()
    {
        taskDisplay = taskManager.taskDisplay;
    }

    private void Update()
    {
        if(!taskCompleted)
        {
            switch(this.name)
                {
                case "车床触发器":
                    utask = lingjianused1.haveshowed;
                    usprite = floatingWindow.imagesc;
                    uniqueTask = taskManager.tasks[1];
                    targetTaskId = uniqueTask.id;
                    break;
                case "激光切割机触发器":
                    utask = polygonDrawer1.haveqiege;
                    usprite = floatingWindow.imagesq;
                    uniqueTask = taskManager.tasks[2];
                    targetTaskId = uniqueTask.id;
                    break;
                case "3D打印机触发器":
                    utask = printdata1.haveprint;
                    usprite = floatingWindow.images3;
                    uniqueTask = taskManager.tasks[3];
                    targetTaskId = uniqueTask.id;
                    break;
                case "组装触发器":
                    utask = assemble1.haveassmble;
                    usprite = floatingWindow.imagesz;
                    uniqueTask = taskManager.tasks[4];
                    targetTaskId = uniqueTask.id;
                    break;
                }
            taskCompleted = utask && taskDisplay.firstIncompleteTask.iscollider;
        }
        if(taskCompleted&&!havefinished)
        {
            CompleteTask();
            havefinished = true;
            taskDisplay.taskText2.text += "(已完成)".ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        taskDisplay = taskManager.taskDisplay;
        // 检查是否是玩家进入触发器，并且任务还未完成
        if (!firsttime)
        {
            if (!floatingWindow.xuanfu.activeSelf)
            {
                floatingWindow.xuanfu.SetActive(true);
                floatingWindow.UpdateFloatingWindow(usprite); // 更新 FloatingWindow 的图片和按钮监听器
                floatingWindow.UpdateImage(usprite);
                floatingWindow.currentimage = usprite;
                firsttime = true;
            }
            else if (floatingWindow.icon.text != "X")
            {
                floatingWindow.RestoreWindow();
                floatingWindow.UpdateFloatingWindow(usprite); // 更新 FloatingWindow 的图片和按钮监听器
                floatingWindow.UpdateImage(usprite);
                floatingWindow.currentimage = usprite;
                firsttime = true;
            }
            else
            {
                floatingWindow.UpdateFloatingWindow(usprite); // 更新 FloatingWindow 的图片和按钮监听器
                floatingWindow.UpdateImage(usprite);
                floatingWindow.currentimage = usprite;
                firsttime = true;
            }
        }
        else
        {
            floatingWindow.UpdateFloatingWindow(usprite); // 更新 FloatingWindow 的图片和按钮监听器
            floatingWindow.UpdateImage(usprite);
            floatingWindow.currentimage = usprite;
        }
        if (other.CompareTag("Player") && !taskCompleted&&!taskDisplay.firstIncompleteTask.iscollider&&taskDisplay.firstIncompleteTask.id==uniqueTask.id)
        {
            if(taskDisplay.taskText.text!="")
            {
                taskDisplay.firstIncompleteTask.iscollider = true;
                taskDisplay.taskText.text += "(已完成)".ToString();
            }
        }
    }

    private void CompleteTask()
    {
        taskDisplay = taskManager.taskDisplay;
        if (taskManager != null && !string.IsNullOrEmpty(targetTaskId)&&targetTaskId==taskDisplay.firstIncompleteTask.id)
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