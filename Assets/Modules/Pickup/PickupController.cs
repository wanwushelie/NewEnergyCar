using System.Runtime.ExceptionServices;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform holdPosition;   // 手持物体的位置
    //public GameObject putdownUI;     // 放下提示UI

    [Header("Debug")]
    [SerializeField] public  GameObject heldObject;  // 当前手持的物体
    private Rigidbody heldObjectRb;  // 手持物体的刚体
    public GameObject targetObject; // 待拾取的物体
    public bool IsHoldingObject=false; // 是否持有物体
    public Camera mainCamera;
    public void Update()
    {
        if(heldObject!=null)
        {
            IsHoldingObject = true;
        }
        else
        {
            IsHoldingObject = false;
        }

    }

    // 设置待拾取的物体
    public void SetTargetObject(GameObject obj)
    {
        targetObject = obj;
    }

    // 拾取物体
    public void Pickup(GameObject obj)
    {
        // if (IsHoldingObject || obj == null) return;
      
        // 获取物体的 ObjectData
        ObjectData objectData = ObjectDataManager.Instance.GetData(obj.name);
        if (objectData == null || !objectData.canBePickedUp)
        {
            return;
        }

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
        objectData.havebeenpicked = true;
        // 显示放下UI

        Debug.Log("已拾取: " + obj.name);
    }

    // 放下物体
    public void PutDown()
    {
        if (heldObject == null) return;
        Debug.Log("PUTDOWN");
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
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
            heldObject.transform.rotation = Quaternion.Euler(euler);
            // 清空引用
            heldObject = null;
            heldObjectRb = null;
        }
    }
}