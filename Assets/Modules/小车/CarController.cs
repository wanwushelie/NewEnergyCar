using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class CarController : MonoBehaviour
{
    public List<Transform> waypoints; // 存储轨迹点的列表
    public float speed = 5f; // 小车的速度
    public int currentWaypointIndex = 0; //  当前目标点的索引
    public Button startButton; // 开始/重置按钮
    public bool isRunning = false; // 控制小车是否运行的标志
    public GameObject main, xiaoche,jiesuanpanel;
    public Text textscore;
    public xiaochedate xiaochedate1;
    void Start()
    {
        // 确保至少有一个轨迹点
        //xiaoche.SetActive(false);
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to CarController.");
            return;
        }

        // 为按钮添加点击事件监听器
        startButton.onClick.AddListener(ToggleCarMovement);
    }

    void Update()
    {
        // 如果小车没有运行，则不执行任何操作
        if (!isRunning)
        {
            //main.SetActive(true);
            //xiaoche.SetActive(false);
            return;
        }
        else
        {
            main.SetActive(false);
            xiaoche.SetActive(true);
        }

        // 如果已经到达最后一个轨迹点，则停止移动
        if (currentWaypointIndex >= waypoints.Count&&isRunning)
        {
            jiesuanpanel.SetActive(true);
            xiaochedate1.jugdement();
            textscore.text = "你最终的得分是" + xiaochedate1.score.ToString();
            isRunning = false; // 到达终点后停止运行
            return;
        }

        // 计算小车当前位置与目标点之间的距离
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // 如果距离小于某个阈值，则认为已经到达目标点，移动到下一个轨迹点
        if (distanceToTarget < 0.1f)
        {
            currentWaypointIndex++;
            return;
        }

        // 计算小车移动的方向和距离
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float moveDistance = speed * Time.deltaTime;

        // 移动小车
        transform.position += moveDirection * moveDistance;

        // 使小车朝向目标点
        transform.LookAt(targetPosition);
    }

    // 切换小车的运行状态
    public void ToggleCarMovement()
    {
        isRunning = !isRunning; // 切换运行状态

        if (isRunning)
        {
            // 如果开始运行，重置小车位置和目标点索引
            
            transform.position = waypoints[0].position;
            currentWaypointIndex = 0;
        }
    }
}