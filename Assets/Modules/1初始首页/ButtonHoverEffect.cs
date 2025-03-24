using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonHoverEffect : MonoBehaviour
{
    public Button[] buttons; // 公开的按钮数组

    private Color originalColor; // 原始颜色
    private Color hoverColor = new Color(0, 0.5f, 1, 1); // 中蓝色背景

    void Start()
    {
        // 遍历按钮并设置初始透明色，同时添加鼠标事件
        foreach (Button button in buttons)
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = new Color(0, 0, 0, 0); // 设置初始颜色为透明
            button.colors = colorBlock;

            // 添加鼠标事件
            button.gameObject.AddComponent<EventTrigger>().triggers = new List<EventTrigger.Entry>();
            AddMouseHoverEvents(button);
        }
    }

    private void AddMouseHoverEvents(Button button)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnter.callback.AddListener((eventData) => { OnPointerEnter(button); });
        trigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExit.callback.AddListener((eventData) => { OnPointerExit(button); });
        trigger.triggers.Add(pointerExit);
    }

    private void OnPointerEnter(Button button)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = hoverColor; // 改变背景为中蓝色
        button.colors = colorBlock;
    }

    private void OnPointerExit(Button button)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = new Color(0, 0, 0, 0); // 恢复为透明色
        button.colors = colorBlock;
    }
}
