using UnityEngine;
using UnityEngine.UI;

public class ToggleComponentsManager : MonoBehaviour
{
    [System.Serializable]
    public struct ToggleGroup
    {
        public GameObject componentToToggle; // 需要控制的组件
        public Button controlButton; // 控制显示/隐藏的按钮
    }

    // 公开变量，可以在Inspector中添加多个组
    public ToggleGroup[] toggleGroups;

    void Start()
    {
        // 为每个按钮添加点击事件
        foreach (var group in toggleGroups)
        {
            if (group.controlButton != null)
            {
                group.controlButton.onClick.AddListener(() => ToggleVisibility(group.componentToToggle));
            }
        }
    }

    void ToggleVisibility(GameObject component)
    {
        // 切换组件的显示状态
        if (component != null)
        {
            component.SetActive(!component.activeSelf);
        }
    }
}
