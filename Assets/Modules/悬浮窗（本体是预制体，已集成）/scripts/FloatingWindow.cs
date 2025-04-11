using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FloatingWindow : MonoBehaviour
{
    //创建一个公开游戏物体变量
    public List<object>[] listArray;
    public GameObject panel;
    public List<Sprite> imagesj,imagesq,images3,imagesz;
    public int currentIndex = 0;
    public Image imageDisplay;
    public Button prevButton;
    public Button nextButton;
    public Button closeButton;
    
    

    public TextMeshProUGUI icon; 

    private bool isMinimized = false;

    private Vector3 offset; // ��¼����������֮���ƫ����
    private Camera cam; // ���ڼ�����������


    void Start()
    {
        listArray = new List<object>[4];
        cam = Camera.main; // ��ȡ�������
        UpdateImage(imagesj);
        // 初始化 listArray 的每个元素
        listArray[0] = new List<object>();
        listArray[1] = new List<object>();
        listArray[2] = new List<object>();
        listArray[3] = new List<object>();
        if (imagesj != null)
        {
            foreach (var sprite in imagesj)
            {
                listArray[0].Add(sprite);
            }
        }
        // 将 imagesq 的内容添加到 listArray[1] 中
        if (imagesq != null)
        {
            foreach (var sprite in imagesq)
            {
                listArray[1].Add(sprite);
            }
        }
        // 将 images3 的内容添加到 listArray[2] 中
        if (images3 != null)
        {
            foreach (var sprite in images3)
            {
                listArray[2].Add(sprite);
            }
        }
        // 将 imageez 的内容添加到 listArray[3] 中
        if (imagesz != null)
        {
            foreach (var sprite in imagesz)
            {
                listArray[3].Add(sprite);
            }
        }
        prevButton.onClick.AddListener(() => ShowPreviousImage(imagesj));
        nextButton.onClick.AddListener(() => ShowNextImage(imagesj));
        closeButton.onClick.AddListener(ToggleMinimize);
    }

    void ShowPreviousImage(List<Sprite> images)
    {
        Debug.Log("pre");
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImage(images);
        }
    }

    void ShowNextImage(List<Sprite> images)
    {
        Debug.Log("Next");
        if (currentIndex < images.Count - 1)
        {
            currentIndex++;
            UpdateImage(images);
        }
    }

    void ToggleMinimize()
    {
        isMinimized = !isMinimized;
        if (isMinimized)
        {
            MinimizeWindow();
        }
        else
        {
            RestoreWindow();
        }
    }


    void MinimizeWindow()
    {
        currentIndex = 0;
        panel.SetActive(false);
        imageDisplay.gameObject.SetActive(false);
        // �������а�ť���������˳���ť
        prevButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
    
        // ֱ���޸�icon�������ı�
        if (icon != null)
        {
            icon.text = "<"; // �޸�Ϊ"<"
        }
    }

    public void RestoreWindow()
    {
        panel.SetActive(true);
        imageDisplay.gameObject.SetActive(true);
        // �ָ����а�ť
        prevButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
    
        // ֱ���޸�icon�������ı�
        if (icon != null)
        {
            icon.text = "X"; // �޸�Ϊ"X"
        }
    }


    public void UpdateImage(List<Sprite>images)
    {
        if (imageDisplay != null && images.Count > 0)
        {
            imageDisplay.sprite = images[currentIndex];
        }
    }




    void OnMouseDown()
    {
        if (isMinimized)
        {
            ToggleMinimize();
        }

        // ��������������֮���ƫ����
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cam.nearClipPlane; // ���������ȷ����ȡ��ȷ����������
        Vector3 worldPosition = cam.ScreenToWorldPoint(mousePosition);
        offset = transform.position - worldPosition;
    }

    void OnMouseDrag()
    {
        if (!isMinimized)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = cam.nearClipPlane; // �������
            Vector3 worldPosition = cam.ScreenToWorldPoint(mousePosition);

            // ����������λ��
            transform.position = worldPosition + offset;
        }
    }
    public  void changeebuttonpre(List<Sprite> images)
    {
        prevButton.onClick.RemoveAllListeners();
        prevButton.onClick.AddListener(() => ShowPreviousImage(images));
    }
    public void changeebuttonnext(List<Sprite> images)
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() => ShowPreviousImage(images));
    }
    public void UpdateFloatingWindow(List<Sprite> newImages)
    {
        // 移除旧的监听器
        prevButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();

        // 添加新的监听器
        prevButton.onClick.AddListener(() => ShowPreviousImage(newImages));
        nextButton.onClick.AddListener(() => ShowNextImage(newImages));

        // 更新图片显示
        currentIndex = 0;
        UpdateImage(newImages);
    }
}






