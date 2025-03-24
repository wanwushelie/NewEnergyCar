using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FloatingWindow : MonoBehaviour
{
    //创建一个公开游戏物体变量
    public GameObject panel;
    public List<Sprite> images; // ��Inspector������ͼƬ�б�
    private int currentIndex = 0;
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
        UpdateImage();

        prevButton.onClick.AddListener(ShowPreviousImage);
        nextButton.onClick.AddListener(ShowNextImage);
        closeButton.onClick.AddListener(ToggleMinimize);
    }

    void ShowPreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImage();
        }
    }

    void ShowNextImage()
    {
        if (currentIndex < images.Count - 1)
        {
            currentIndex++;
            UpdateImage();
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
        // �������а�ť���������˳���ť
        prevButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
    
        // ֱ���޸�icon�������ı�
        if (icon != null)
        {
            icon.text = "<"; // �޸�Ϊ"<"
        }
    }

    void RestoreWindow()
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


    void UpdateImage()
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
}






