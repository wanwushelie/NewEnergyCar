using UnityEngine;

public class CentralRayController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform currentTarget; // 当前高亮的目标物体

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
            string objectName = hit.collider.gameObject.name;
            ObjectData objectData = ObjectDataManager.Instance.GetData(objectName);

            if (objectData != null)
            {
                // 如果命中了新的目标物体或当前物体数据发生变化
                if (hit.collider.gameObject.transform != currentTarget)
                {
                    // 关闭旧的高亮和UI
                    if (currentTarget != null)
                    {
                        HighlightObject(currentTarget.gameObject, false, Color.clear); // 关闭高亮
                        HideUI();
                    }

                    // 更新当前高亮目标
                    currentTarget = hit.collider.gameObject.transform;

                    // 设置新的高亮和UI
                    HighlightObject(hit.collider.gameObject, objectData.canBeHighlighted, objectData.highlightColor);
                    if (objectData.showUI)
                    {
                        ShowUI(objectData.uiPrefab);
                    }
                }
                else
                {
                    // 如果还是同一个目标物体，确保高亮和UI状态持续
                    HighlightObject(hit.collider.gameObject, objectData.canBeHighlighted, objectData.highlightColor);
                    if (objectData.showUI)
                    {
                        ShowUI(objectData.uiPrefab);
                    }
                    else
                    {
                        HideUI(); // 如果当前物体不显示UI，则隐藏UI
                    }
                }
            }
        }
        else
        {
            // 如果没有命中任何物体，关闭当前高亮目标的高亮状态和UI
            if (currentTarget != null)
            {
                HighlightObject(currentTarget.gameObject, false, Color.clear); // 关闭高亮
                HideUI();
                currentTarget = null;
            }
        }
    }

    private void HighlightObject(GameObject target, bool shouldHighlight, Color highlightColor)
    {
        HighlightableObject highlightableObject = target.GetComponent<HighlightableObject>();
        if (highlightableObject != null)
        {
            if (shouldHighlight)
            {
                highlightableObject.On(highlightColor); // 开启高亮
            }
            else
            {
                highlightableObject.Off(); // 关闭高亮
            }
        }
        else
        {
            Debug.LogWarning($"No HighlightableObject component found on {target.name}");
        }
    }

    private void ShowUI(GameObject uiPrefab)
    {
        UIController.Instance.ShowUI(uiPrefab);
    }

    private void HideUI()
    {
        UIController.Instance.HideUI();
    }
}