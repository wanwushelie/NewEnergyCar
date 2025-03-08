using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;          // 水平移动速度
    public float verticalSpeed = 2f;      // 上下移动速度
    public float mouseSensitivity = 3f;  // 鼠标灵敏度
    public float verticalLimit = 80f;    // 垂直旋转的限制角度

    private float rotationX = 0f;        // 俯仰角
    private Quaternion initialRotation;  // 初始旋转角度
    private bool isDragging = false;     // 是否正在按住右键拖动


    void Update()
    {
        HandleHorizontalMovement();       // 处理水平移动逻辑
        HandleVerticalMovement();         // 处理上下移动逻辑
        HandleMouseLook();                // 处理鼠标视角控制
    }

    void HandleHorizontalMovement()
    {
        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveRight = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // 计算水平方向的移动方向
        Quaternion horizontalRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        Vector3 forward = horizontalRotation * Vector3.forward;
        Vector3 right = horizontalRotation * Vector3.right;

        // 仅在 X 和 Z 轴上移动
        Vector3 movement = forward * moveForward + right * moveRight;
        movement.y = 0; // 确保没有垂直移动

        transform.position += movement;
    }

    void HandleVerticalMovement()
    {
        // 上下移动通过 Q 和 E 键控制
        float moveVertical = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            moveVertical -= verticalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            moveVertical += verticalSpeed * Time.deltaTime;
        }

        // 只在 Y 轴方向上移动
        Vector3 verticalMovement = new Vector3(0, moveVertical, 0);
        transform.position += verticalMovement;
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