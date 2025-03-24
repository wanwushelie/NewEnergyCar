using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOK : MonoBehaviour
{
    public GameObject imagePanel; // �洢��ʾͼƬ�����
    public GameObject hidePanel;
    public GameObject otherPanel;
    public Button OK; // ��ʾͼƬ�İ�ť
    public Button NO; // ����ͼƬ�İ�ť
    public Button YES; // ��ת���������İ�ť
    public bool switchScene = false; // �Ƿ��л�����
    public string targetSceneName;// Ŀ�곡��

    void Start()
    {
        // Ϊ��ʾͼƬ�İ�ť���ӵ���¼�
        OK.onClick.AddListener(ShowImage);

        // Ϊ����ͼƬ�İ�ť���ӵ���¼�
        NO.onClick.AddListener(HideImage);

        // Ϊ��ת���������İ�ť���ӵ���¼�
        YES.onClick.AddListener(GoToOtherPanel);

        // ��ʼʱ����ͼƬ���
        imagePanel.SetActive(false);
    }

    // ��ʾͼƬ���
    void ShowImage()
    {
        imagePanel.SetActive(true);
        Debug.Log("��ʾͼƬ��ť�����");
    }

    // ����ͼƬ���
    void HideImage()
    {
        imagePanel.SetActive(false);
        Debug.Log("����ͼƬ��ť�����");
    }

    // ��ת���������
    void GoToOtherPanel()
    {
        imagePanel.SetActive(false); // ���ص�ǰͼƬ���
        Debug.Log("��ת��������尴ť�����");
        // ��ת�������
        if (switchScene)
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            // ��ʾ�������
            if (!switchScene)
            {
                hidePanel.SetActive(false);
                otherPanel.SetActive(true);
            }
        }
    }
}

