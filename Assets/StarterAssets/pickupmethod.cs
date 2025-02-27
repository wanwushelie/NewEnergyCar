using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class pickupmethod : MonoBehaviour
{
    public GameObject pickupUIPrefab;   // ʰȡUIԤ����
    public PickupController pickupController; // ʰȡ������

    private GameObject pickupUI;        // ��̬���ɵ�UIʵ��
    private Button pickupButton;        // ʰȡ��ť���
    private Text objectNameText;        // UI�е��ı����
    private GameObject targetObject;// ��ǰ��ʰȡ������
    public string pickupname;
    public GameObject dizuo, chelun, tulun1,tulun2;
    Vector3 spawnPosition; // ʰȡ��Ʒ��λ��  
    Quaternion spawnRotation = Quaternion.identity; // Ĭ��Ϊ����ת
    public GameObject hideobject;
    public GameObject chaixieUI,chelunpart,tulunpart,dizuopart;//��ʾui
    private bool ischelun=false, isdizuo=false, istulun=false;//�ж�С�����Ƿ�������
    public  List<GameObject> HideGameObjects = new List<GameObject>();
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
                pickupname = hitObject.name;
                spawnPosition = hitObject.transform.position;
                spawnRotation = hitObject.transform.rotation;//����ʰȡ����λ��
                hideobject=Instantiate(hitObject, spawnPosition, spawnRotation);
                HideGameObjects.Add(hideobject);
                hideobject.SetActive(false);
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
            if (pickupController.IsHoldingObject && hitObject.name == "��еС��δ���")//��װС������
            {
                Assemble(pickupController.heldObject);
            }
            if(!pickupController.IsHoldingObject&& hitObject.name == "��еС��δ���")//��жС������
            {
                chaixieUI.SetActive(true);
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
    void Assemble(GameObject car)
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (car.name)
            {
                case "tulun":
                    tulun1.SetActive(true);
                    tulun2.SetActive(true);
                    tulunpart.SetActive(true)
;                    istulun = true;
                    break;
                case "chelun":
                    chelun.SetActive(true);
                    ischelun = true;
                    chelunpart.SetActive(true);
                    break;
                case "dizuo":
                    dizuo.SetActive(true);
                    isdizuo = true;
                    dizuopart.SetActive(true);
                    break;
            }
            Debug.Log("��ƴװ: " + car.name);
            Destroy(car);
        }
    }
    public void Disassemblychelun()
    {
        if (ischelun)
        { 
        GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name == "chelun(Clone)");
        foundObject.SetActive(true);
        chelun.SetActive(false);
        HideGameObjects.Remove(foundObject);
    }
        else
        {

        }
        ischelun = false;
    }
    public void Disassemblytulun()
    {
        if (istulun)
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name == "tulun(Clone)");
            foundObject.SetActive(true);
            tulun1.SetActive(false);
            tulun2.SetActive(false);
            HideGameObjects.Remove(foundObject);
        }
        else
        {

        }
        istulun = false;
    }
    public void Disassemblydizuo()
    {
        if (isdizuo)
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name == "dizuo(Clone)");
            foundObject.SetActive(true);
            dizuo.SetActive(false);
            HideGameObjects.Remove(foundObject);
        }
        else
        {

        }
        isdizuo = false;
    }
}