using UnityEngine;

public class HighlightAndUIShow : MonoBehaviour
{
    public GameObject uiPrefab; // 每个物体对应的UI预制体
    public LayerMask showingLayer; // 高亮显示的Layer
    public string showingTag = "UIshowing"; // 触发UI显示的Tag

    private HighlightableObject highlightableObject; // 高亮组件引用
    private bool isHighlighted = false; // 当前是否高亮
    private bool isUIShowing = false; // 当前是否显示UI

    private void Awake()
    {
        highlightableObject = GetComponent<HighlightableObject>();
    }

    // 尝试高亮
    public void TryHighlight(bool shouldHighlight)
    {
        // 如果物体的Layer是showingLayer，则允许高亮
        if (showingLayer == (showingLayer | (1 << gameObject.layer)))
        {
            if (shouldHighlight && !isHighlighted)
            {
                highlightableObject.On(Color.red); // 开启高亮
                isHighlighted = true;
            }
            else if (!shouldHighlight && isHighlighted)
            {
                highlightableObject.Off(); // 关闭高亮
                isHighlighted = false;
            }
        }
    }

    // 尝试显示UI
    public void TryShowUI(bool shouldShowUI)
    {
        // 如果物体的Tag是showingTag，则显示UI
        if (gameObject.CompareTag(showingTag))
        {
            if (shouldShowUI && !isUIShowing)
            {
                UIController.Instance.ShowUI(uiPrefab); // 显示UI
                isUIShowing = true;
            }
            else if (!shouldShowUI && isUIShowing)
            {
                UIController.Instance.HideUI(); // 隐藏UI
                isUIShowing = false;
            }
        }
    }
}