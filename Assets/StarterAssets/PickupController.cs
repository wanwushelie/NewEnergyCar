using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform rightWrist; // 右手腕的位置
    public LayerMask pickUpLayer; // 物体所在的层
    public GameObject handSphere; // 手上的圆球

    void Start()
    {
        // 初始化逻辑，如果需要的话
    }

    void Update()
    {
        // 移动和其他控制逻辑，如果需要的话
    }

    public void Pickup(GameObject objectToPickup)
    {
        objectToPickup.transform.SetParent(rightWrist); // 将物体附着到右手腕
        objectToPickup.transform.localPosition = Vector3.zero; // 设置局部位置为原点
        objectToPickup.transform.localRotation = Quaternion.identity; // 设置局部旋转为默认值

        // 关闭UI
        Destroy(objectToPickup.GetComponent<PickupObject>().pickupUI);

        // 隐藏手上的圆球
        if (handSphere != null)
        {
            handSphere.SetActive(false);
        }
    }
}



