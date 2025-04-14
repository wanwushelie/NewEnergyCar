using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FloatingWindow : MonoBehaviour
{
    //创建一个公开游戏物体变量
    public List<Sprite>[] listArray;
    public GameObject panel,xuanfu;
    public List<Sprite> imagesc,imagesq,images3,imagesz,currentimage; // ��Inspector������ͼƬ�б�
    public int currentIndex = 0;
    public Image imageDisplay;
    public Button prevButton;
    public Button nextButton;
    public Button closeButton;

    public TextMeshProUGUI icon; // �޸�ΪTextMeshProUGUI��������һ��������Text���������ڰ��˳���ť���ı�

    private bool isMinimized = false;

    private Vector3 offset; // ��¼����������֮���ƫ����
    private Camera cam; // ���ڼ�����������


    void Start()
    {
        cam = Camera.main; // ��ȡ�������
        UpdateImage(imagesc);
        listArray = new List<Sprite>[4];
        if (currentimage==null)
        {
            prevButton.onClick.AddListener(() => ShowPreviousImage(currentimage));
            nextButton.onClick.AddListener(() => ShowNextImage(currentimage));
        }
        closeButton.onClick.AddListener(ToggleMinimize);
        for (int i = 0; i < listArray.Length; i++)
        {
            listArray[i] = new List<Sprite>();
        }
        if (imagesc != null)
        {
            foreach (var sprite in imagesc)
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

        // 将 imagesz 的内容添加到 listArray[3] 中
        if (imagesz != null)
        {
            foreach (var sprite in imagesz)
            {
                listArray[3].Add(sprite);
            }
        }

    }
    private void Update()
    {if (currentimage!=null)
        {
            isstart(currentimage);
        }
    }
    void ShowPreviousImage(List<Sprite> images)
    {
        if (currentIndex > 0)
        {
            Debug.Log("pre");
            currentIndex--;
            UpdateImage(images);
        }
    }

    void ShowNextImage(List<Sprite>  images)
    {
        if (currentIndex < images.Count - 1)
        {
            Debug.Log("next");
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
        panel.SetActive(false);
        imageDisplay.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        currentIndex = 0;
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


    public void UpdateImage(List<Sprite> images)
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
    void isstart(List<Sprite> images)
    {
        int i = 0;
        for(i=0;i<images.Count;i++)
        {
            if (imageDisplay.sprite == images[i])
            {
                break;
            }
            if(i==images.Count-1)
            {
                UpdateImage(currentimage);
                prevButton.onClick.AddListener(() => ShowPreviousImage(images));
                nextButton.onClick.AddListener(() => ShowNextImage(images));
            }
        }
    }

}






