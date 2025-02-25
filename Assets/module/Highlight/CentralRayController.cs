using UnityEngine;

public class CentralRayController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform currentTarget; //  当前高亮的目标物体

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            HighlightAndUIShow target = hit.collider.gameObject.GetComponent<HighlightAndUIShow>();
            if (target != null)
            {
                // 如果命中了新的目标物体
                if (target != currentTarget)
                {
                    // 关闭当前高亮目标的高亮状态
                    if (currentTarget != null)
                    {
                        currentTarget.GetComponent<HighlightAndUIShow>().TryHighlight(false);
                        currentTarget.GetComponent<HighlightAndUIShow>().TryShowUI(false);
                    }

                    // 更新当前高亮目标
                    currentTarget = target.transform;
                    target.TryHighlight(true); // 开启高亮
                    target.TryShowUI(true); // 显示UI
                }
            }
        }
        else
        {
            // 如果没有命中任何物体，关闭当前高亮目标的高亮状态
            if (currentTarget != null)
            {
                currentTarget.GetComponent<HighlightAndUIShow>().TryHighlight(false);
                currentTarget.GetComponent<HighlightAndUIShow>().TryShowUI(false);
                currentTarget = null;
            }
        }
    }
}