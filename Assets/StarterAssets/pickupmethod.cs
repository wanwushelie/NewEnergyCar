using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pickupmethod : MonoBehaviour
{
    public GameObject pickupUIPrefab;   // ʰȡUIԤ����
    public PickupController pickupController; // ʰȡ������

    private GameObject pickupUI;        // ��̬���ɵ�UIʵ��
    private Button pickupButton;        // ʰȡ��ť���
    private Text objectNameText;        // UI�е��ı����
    private GameObject targetObject;    // ��ǰ��ʰȡ������

    void Start()
    {
        InitializePickupUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForClickableObject();
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        closeputdown();
    }

    // ��ʼ��ʰȡUI
    void InitializePickupUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("������δ�ҵ�Canvas��");
            return;
        }

        // ʵ����UI����ȡ���
        pickupUI = Instantiate(pickupUIPrefab, canvas.transform);
        pickupButton = pickupUI.GetComponentInChildren<Button>();
        objectNameText = pickupUI.GetComponentInChildren<Text>();
        pickupUI.SetActive(false);

        // �󶨰�ť�¼�
        if (pickupButton != null)
        {
            pickupButton = pickupUI.GetComponentInChildren<Button>();
            pickupButton.onClick.AddListener(OnPickupButtonClicked);
        }
        else
        {
            Debug.LogError("ʰȡUI��δ�ҵ�Button�����");
        }
    }

    // ��������������
    void CheckForClickableObject()
    {
        if (EventSystem.current.IsPointerOverGameObject()&& !pickupController.IsHoldingObject) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // ���������ǿ�ʰȡ�����ҵ�ǰδ��������
            if (IsPickupable(hitObject) && !pickupController.IsHoldingObject)
            {
                targetObject = hitObject;
                ShowPickupUI(hitObject.name);
            }
            else
            {
                ClearTargetAndHideUI();
            }
            if(pickupController.IsHoldingObject&&hit.collider.CompareTag("xiaoche"))
            {
                pickupController.PutDown();
            }
          
        }
        else
        {
            ClearTargetAndHideUI();
        }
    }

    // �ж������Ƿ��ʰȡ
    bool IsPickupable(GameObject obj)
    {
        return obj.CompareTag("Pickupable") && obj != pickupController.targetObject;
    }

    // ��ʾʰȡUI
    void ShowPickupUI(string objectName)
    {
        objectNameText.text = "ʰȡ: " + objectName;
        pickupUI.SetActive(true);
    }

    // ���ʰȡ��ťʱ���߼�
    public void OnPickupButtonClicked()
    {
        if (targetObject != null)
        {
            pickupController.Pickup(targetObject);
            ClearTargetAndHideUI();
        }
        Debug.Log("��ť�������");
    }

    // ���Ŀ�겢����UI
    void ClearTargetAndHideUI()
    {
        targetObject = null;
        pickupUI.SetActive(false);
    }
    void closeputdown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (pickupController.IsHoldingObject && !hit.collider.CompareTag("xiaoche"))
            {
                pickupController.putdownUI.SetActive(false);
            }
        }
       
    }
}