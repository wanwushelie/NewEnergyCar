using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float speed = 5.0f;       // 小车的前进速度
    public float turnSpeed = 100.0f; // 小车的转向速度（每秒旋转的度数）

    private Rigidbody rb;           // 用于控制小车运动的Rigidbody组件

    void Start()
    {
        // 获取Rigidbody组件
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 匀速前进
        rb.velocity = transform.forward * speed;

        // 转向（这里可以添加转向逻辑，例如通过输入控制）
        // 示例：每秒向右旋转turnSpeed度
        transform.Rotate(0, turnSpeed * Time.fixedDeltaTime, 0);
    }
}