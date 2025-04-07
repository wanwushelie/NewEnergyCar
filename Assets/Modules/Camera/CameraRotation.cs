using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Start is called before the first frame update

    public float mouseSensitivity = 3f;  // 鼠标灵敏度
    public float verticalLimit = 80f;    // 垂直旋转的限制角度

    private float rotationX = 0f;        // 俯仰角
    private bool isDragging = false;     // 是否正在按住右键拖动

    private void Update()
    {
        HandleMouseLook();               // 处理鼠标视角控制
    }

    void HandleMouseLook()
    {
        // 只有在按下右键时才允许转动视角
        if (Input.GetMouseButtonDown(1)) // 鼠标右键按下
        {
            isDragging = true;
            // 锁定鼠标指针
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonUp(1)) // 鼠标右键松开
        {
            isDragging = false;
            // 解锁鼠标指针
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (isDragging)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // 相机水平旋转
            transform.Rotate(0, mouseX, 0);

            // 相机垂直旋转
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -verticalLimit, verticalLimit);  // 限制垂直旋转角度
            transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, 0);
        }
    }
}
