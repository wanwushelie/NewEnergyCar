using UnityEngine;
using UnityEngine.UI;

public class ImageDisplayManager : MonoBehaviour
{
    public Image[] imageUIs; // 存储所有的图片UI
    public Button[] buttons; // 存储对应的按钮

    private void Start()
    {
        // 为每个按钮添加点击事件
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 避免闭包问题
            buttons[i].onClick.AddListener(() => ShowImage(index));
        }

        // 初始化时隐藏所有图片
        HideAllImages();
    }

    private void ShowImage(int index)
    {
        // 先隐藏所有图片
        HideAllImages();

        // 显示选中索引对应的图片
        if (index >= 0 && index < imageUIs.Length)
        {
            imageUIs[index].gameObject.SetActive(true); // 显示对应的图片
        }
    }

    private void HideAllImages()
    {
        // 遍历所有图片并隐藏
        foreach (var image in imageUIs)
        {
            image.gameObject.SetActive(false);
        }
    }
}
