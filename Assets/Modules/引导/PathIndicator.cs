using UnityEngine;

public class PathIndicator : MonoBehaviour
{
    public Transform target; // 目标物体的Transform
    public LineRenderer lineRenderer; // LineRenderer组件

    private Transform player; // 主角的Transform

    // Start is called before the first frame update
    void Start()
    {
        player = transform; // 假设这个脚本附加在主角上
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer组件未设置！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // 设置LineRenderer的起点和终点
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, target.position);
        }
    }
}