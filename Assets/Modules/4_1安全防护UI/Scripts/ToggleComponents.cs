using UnityEngine;
using UnityEngine.UI;

public class ToggleComponents : MonoBehaviour
{
    // 可在编辑器中设置的公开变量
    public GameObject componentToHide; // 需要隐藏的组件（图片、Panel等）
    public GameObject componentToShow; // 需要显示的组件
    public Button toggleButton; // 控制切换的按钮

    void Start()
    {
        // 确保组件的初始状态
        if (componentToHide != null)
            componentToHide.SetActive(true);

        if (componentToShow != null)
            componentToShow.SetActive(false);

        // 为按钮添加点击事件
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleComponentsVisibility);
    }

    void ToggleComponentsVisibility()
    {
        if (componentToHide != null && componentToShow != null)
        {
            // 切换组件的显示状态
            bool isHiddenActive = componentToHide.activeSelf;
            componentToHide.SetActive(!isHiddenActive);
            componentToShow.SetActive(isHiddenActive);
        }
    }
}

