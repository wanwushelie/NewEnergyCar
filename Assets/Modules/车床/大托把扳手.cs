using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 大托把扳手 : MonoBehaviour
{
    public float sensitivity = 100f; // 旋转灵敏度
    public float moveSpeed = 5f;     // 沿X轴移动的速度
    public Transform objectToRotate; // 要旋转的物体
    public Transform objectToMove;   // 要移动的物体
    public LayerMask targetLayer;    // 目标物体所在的图层
    public float rayDistance = 10000f; // 射线的最大距离
    public float maxMoveDistance = 0.2f; // 最大移动距离

    private bool isDragging = false; // 是否正在拖动
    private float nowXposition;     // 物体X位置
    private float initialXPosition;  // 物体初始X位置
    public Camera mainCamera;
    private float initialMouseX;     // 鼠标初始X位置

    private void Start()
    {
        initialXPosition = objectToMove.position.x; // 记录初始X位置
    }

    void Update()
    {
        // 如果尚未开始拖动，检测鼠标按下时是否命中目标物体
        if (!isDragging)
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, rayDistance, targetLayer) && hit.transform == objectToRotate)
            {
                if (Input.GetMouseButtonDown(0)) // 按下鼠标左键时记录初始位置
                {
                    initialMouseX = Input.mousePosition.x;
                    isDragging = true;
                }
            }
        }

        // 如果已经开始拖动
        if (isDragging)
        {
            float mouseX = Input.mousePosition.x;
            float isX = mouseX - initialMouseX;
            nowXposition = objectToMove.position.x;

            // 计算新的X位置
            float deltaX = nowXposition - initialXPosition;

            // 更新物体位置
            //objectToMove.position = new Vector3(newXPosition, objectToMove.position.y, objectToMove.position.z);

            // 根据鼠标移动方向更新旋转
            if (isX > 0&&initialXPosition-nowXposition<=maxMoveDistance) // 鼠标向右移动
            {
                objectToMove.position -= Vector3.right * moveSpeed * Time.deltaTime;
                objectToRotate.Rotate(Vector3.forward, sensitivity * Time.deltaTime);
            }
            else if (isX < 0&&initialXPosition - nowXposition >= -maxMoveDistance) // 鼠标向左移动
            {
                objectToMove.position += Vector3.right * moveSpeed * Time.deltaTime;
                objectToRotate.Rotate(Vector3.forward, -sensitivity * Time.deltaTime);
            }

           
        }

        // 如果释放鼠标左键，停止拖动
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}