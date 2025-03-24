using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour
{
    // 公开变量，可以在Inspector面板中赋值
    public Sprite normalImage; // 正常状态下的图片
    public Sprite hoveredImage; // 悬浮状态下的图片
    public Sprite pressedImage; // 按下状态下的图片

    private Image buttonImage; // 按钮的Image组件

    void Start()
    {
        // 获取按钮的Image组件
        buttonImage = GetComponent<Image>();

        // 初始状态下设置为透明背景
        if (normalImage != null)
        {
            buttonImage.sprite = normalImage;
        }
        else
        {
            buttonImage.color = new Color(0, 0, 0, 0); // 设置颜色为完全透明
        }

        // 添加事件监听器
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnEnable()
    {
        // 添加事件触发器监听器
        var trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entryHoverEnter = new EventTrigger.Entry();
        entryHoverEnter.eventID = EventTriggerType.PointerEnter;
        entryHoverEnter.callback.AddListener((data) => OnPointerEnter());
        trigger.triggers.Add(entryHoverEnter);

        EventTrigger.Entry entryHoverExit = new EventTrigger.Entry();
        entryHoverExit.eventID = EventTriggerType.PointerExit;
        entryHoverExit.callback.AddListener((data) => OnPointerExit());
        trigger.triggers.Add(entryHoverExit);
    }

    void OnDisable()
    {
        // 移除事件触发器监听器
        Destroy(GetComponent<EventTrigger>());
    }

    void OnPointerEnter()
    {
        if (hoveredImage != null)
        {
            buttonImage.color = new Color(255, 255, 255, 255); // 设置颜色为完全显示
            buttonImage.sprite = hoveredImage;
        }
    }

    void OnPointerExit()
    {
        if (normalImage != null)
        {
            buttonImage.sprite = normalImage;
        }
        else
        {
            buttonImage.color = new Color(0, 0, 0, 0); // 设置颜色为完全透明
        }
    }

    void OnButtonClick()
    {
        if (pressedImage != null)
        {
            buttonImage.sprite = pressedImage;
        }
    }
}



