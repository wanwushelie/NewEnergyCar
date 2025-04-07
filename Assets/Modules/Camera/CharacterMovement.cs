using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;          // 水平移动速度
    public float verticalSpeed = 2f;      // 上下移动速度
    public bool canMove = true;           // 控制角色是否可以移动
    public Transform cameraTransform;     // 摄像机的 Transform，用于获取方向

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleHorizontalMovement();   // 处理水平移动逻辑
            HandleVerticalMovement();     // 处理上下移动逻辑
        }
    }

    void HandleHorizontalMovement()
    {
        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveRight = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // 使用摄像机的方向作为移动方向
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

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
}

