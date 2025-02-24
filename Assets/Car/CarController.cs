using UnityEngine;

public class CarController : MonoBehaviour
{
    // 车轮碰撞体引用
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    // 驱动参数
    public float motorTorque = 500f;  // 驱动力

    // 悬挂参数
    public float suspensionSpring = 30000f;  // 悬挂弹簧硬度
    public float suspensionDamper = 4500f;   // 悬挂阻尼
    public float suspensionDistance = 0.3f;  // 悬挂压缩距离

    // 车辆重心调整
    public Vector3 centerOfMass = new Vector3(0, -0.5f, 0);

    // 车轮位置调整
    private Transform[] wheelTransforms;

    void Start()
    {
        // 初始化车轮位置
        wheelTransforms = new Transform[] { frontLeftWheel.transform, frontRightWheel.transform, rearLeftWheel.transform, rearRightWheel.transform };

        // 设置悬挂参数
        SetSuspensionParameters();

        // 调整车体重心
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    void FixedUpdate()
    {
        // 给后轮施加恒定的驱动力
        rearLeftWheel.motorTorque = motorTorque;
        rearRightWheel.motorTorque = motorTorque;

        // 动态调整车轮位置
        AdjustWheelPositions();
    }

    void SetSuspensionParameters()
    {
        // 设置悬挂参数
        JointSpring spring = new JointSpring();
        spring.spring = suspensionSpring;
        spring.damper = suspensionDamper;
        spring.targetPosition = 0;

        // 应用到所有车轮
        frontLeftWheel.suspensionSpring = spring;
        frontLeftWheel.suspensionDistance = suspensionDistance;

        frontRightWheel.suspensionSpring = spring;
        frontRightWheel.suspensionDistance = suspensionDistance;

        rearLeftWheel.suspensionSpring = spring;
        rearLeftWheel.suspensionDistance = suspensionDistance;

        rearRightWheel.suspensionSpring = spring;
        rearRightWheel.suspensionDistance = suspensionDistance;
    }

    void AdjustWheelPositions()
    {
        // 确保车轮始终与地面接触
        foreach (WheelCollider wc in new WheelCollider[] { frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel })
        {
            Vector3 colliderCenter = wc.transform.TransformPoint(wc.center);
            RaycastHit hit;
            if (Physics.Raycast(colliderCenter, -wc.transform.up, out hit, wc.suspensionDistance + wc.radius))
            {
                wc.transform.position = hit.point + wc.transform.up * wc.radius;
            }
        }
    }
}