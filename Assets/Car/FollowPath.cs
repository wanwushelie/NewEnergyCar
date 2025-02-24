using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public LineRenderer path; // 指定路径
    public float speed = 5.0f; // 移动速度
    public float rotationSpeed = 5.0f; // 转向速度
    private int currentPointIndex = 0;

    void Update()
    {
        Vector3[] points = new Vector3[path.positionCount];
        path.GetPositions(points);

        if (currentPointIndex < points.Length - 1)
        {
            Vector3 targetPoint = points[currentPointIndex + 1];
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

            // 计算路径方向并转向
            Vector3 direction = (targetPoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 判断是否到达下一个点
            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                currentPointIndex++;
            }
        }
    }
}