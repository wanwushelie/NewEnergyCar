using UnityEngine;
using UnityEngine.UI;

public class ImageControl : MonoBehaviour
{
    // 公共变量，方便在Inspector面板中设置
    public Button showButton;  // 用于显示图片的按钮
    public Button hideButton;  // 用于隐藏图片的按钮
    public GameObject imageObject;  // 要控制显示或隐藏的图片

    void Start()
    {
        // 给按钮添加监听事件
        showButton.onClick.AddListener(ShowImage);
        hideButton.onClick.AddListener(HideImage);

        // 默认隐藏图片
        imageObject.SetActive(false);
    }

    // 显示图片的方法
    public void ShowImage()
    {
        imageObject.SetActive(true);  // 设置图片为显示状态
    }

    // 隐藏图片的方法
    public void HideImage()
    {
        imageObject.SetActive(false);  // 设置图片为隐藏状态
    }
}
