using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // 角色移动速度
    private Rigidbody rb; // 角色的Rigidbody组件

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // 获取水平方向输入
        float z = Input.GetAxis("Vertical"); // 获取垂直方向输入

        Vector3 move = transform.right * x + transform.forward * z;

        rb.MovePosition(rb.position + move * walkSpeed * Time.deltaTime);
    }
}