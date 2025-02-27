using System.Runtime.ExceptionServices;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform holdPosition;   // 手持物体的位置
    public GameObject putdownUI;     // 放下提示UI
    public LayerMask pickUpLayer;    // 可拾取物体的层
    public GameObject handSphere;    // 手上的圆球（可选）

    [Header("Debug")]
    [SerializeField] public  GameObject heldObject;  // 当前手持的物体
    private Rigidbody heldObjectRb;  // 手持物体的刚体
    public GameObject targetObject; // 待拾取的物体
    public bool IsHoldingObject => heldObject != null; // 是否持有物体

    void Start()
    {
       //holdPosition.position = handSphere.transform.position;
    }

    // 设置待拾取的物体
    public void SetTargetObject(GameObject obj)
    {
        targetObject = obj;
    }

    // 拾取物体
    public void Pickup(GameObject obj)
    {
        if (IsHoldingObject || obj == null) return;

        // 设置手持物体
        heldObject = obj;
        heldObjectRb = heldObject.GetComponent<Rigidbody>();

        // 禁用物理模拟
        if (heldObjectRb != null)
        {
            heldObjectRb.isKinematic = true;
        }

        // 将物体移动到手持位置并设置父物体
        heldObject.transform.position = holdPosition.position;
        heldObject.transform.parent = holdPosition;
        if (handSphere != null)
        {
            handSphere.SetActive(false);
        }

        // 显示放下UI
        if (putdownUI != null)
        {
            putdownUI.SetActive(true);
        }

        Debug.Log("已拾取: " + obj.name);
    }

    // 放下物体
    public void PutDown()
    {
        if (heldObject == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 计算放置位置
            Vector3 surfaceNormal = hit.normal;
            Vector3 putDownPosition = hit.point + surfaceNormal * heldObject.GetComponent<Collider>().bounds.extents.y;
           
            // 恢复物理和父物体
            if (heldObjectRb != null)
            {
                heldObjectRb.isKinematic = false;
            }
            heldObject.transform.parent = null;
            heldObject.transform.position = putDownPosition;
            heldObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
            Vector3 euler = targetRotation.eulerAngles;
            euler.x = -90;
            euler.y = 0; // 锁定 X 轴旋转
            euler.z = 0; // 锁定 Z 轴旋转
            heldObject.transform.rotation = Quaternion.Euler(euler);
            // 清空引用
            heldObject = null;
            heldObjectRb = null;
        }

        // 隐藏放下UI
        putdownUI.SetActive(false);
    }

    // 生成隐藏圆球（可选功能）
    public void GenerateHiddenBall()
    {
        if (handSphere == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            handSphere.transform.position = hit.point;
            handSphere.SetActive(true);
        }
    }
}