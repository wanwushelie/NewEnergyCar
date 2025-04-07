using System.Threading.Tasks;
using UnityEngine;

public class TaskCompletionOnTrigger : MonoBehaviour
{
    // 用于指定要完成的任务 ID
    public string targetTaskId;
    // 标志位，用于判断任务是否已经完成
    private bool taskCompleted = false;
    public TaskDisplay taskDisplay;
    public bool utask,havefinished;
    public printdata printdata1;
    public PolygonDrawer polygonDrawer1;
    public lingjianused lingjianused1;
    public Assemble assemble1;
    private void Update()
    {
        if(!taskCompleted)
        {
            switch(this.name)
                {
                case "车床触发器":
                    utask = lingjianused1.haveshowed;
                    break;
                case "激光切割机触发器":
                    utask = polygonDrawer1.haveqiege;
                    break;
                case "3D打印机触发器":
                    utask = printdata1.haveprint;
                    break;
                case "组装触发器":
                    utask = assemble1.haveassmble;
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
        TaskManager taskManager = TaskManager.Instance;
        taskDisplay = taskManager.taskDisplay;
        // 检查是否是玩家进入触发器，并且任务还未完成
        if (other.CompareTag("Player") && !taskCompleted&&!taskDisplay.firstIncompleteTask.iscollider)
        {
            taskDisplay.firstIncompleteTask.iscollider = true;
            taskDisplay.taskText.text += "(已完成)".ToString();
           
        }
    }

    private void CompleteTask()
    {
        TaskManager taskManager = TaskManager.Instance;
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