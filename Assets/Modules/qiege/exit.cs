using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    public GameObject panel; // 在 Inspector 中设置要控制的面板

    private bool isPanelActive = true; // 面板的初始状态为显示

    void Start()
    {
        // 确保面板在开始时是显示的
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    void Update()
    {
        // 检测 ESC 键是否被按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        // 切换面板的显示状态
        if (panel != null)
        {
            isPanelActive = !isPanelActive;
            panel.SetActive(isPanelActive);
        }
        else
        {
            Debug.LogError("Panel is not assigned in the Inspector.");
        }
    }
}
