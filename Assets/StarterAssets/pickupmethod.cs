using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

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
    public  bool ischelun=false, isdizuo=false, istulun=false;//�ж�С�����Ƿ�������
    public  List<GameObject> HideGameObjects = new List<GameObject>();
    public xiaochedate Xiaochedate;
     
   
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
                spawnPosition = hitObject.transform.position;
                spawnRotation = hitObject.transform.rotation;//����ʰȡ����λ��
                //hideobject = Instantiate(hitObject, spawnPosition, spawnRotation);
                //hideobject.SetActive(false);
                HideGameObjects.Add(hitObject);
                pickupname = hitObject.name;
                ShowPickupUI(hitObject.name);
            }
            else
            {
                ClearTargetAndHideUI();
            }
            if(pickupController.IsHoldingObject&&hit.collider.CompareTag("xiaoche"))//��װС������
            
            {
                pickupController.PutDown();
               
            }
            if (pickupController.IsHoldingObject && hitObject.name == "��еС��δ���"&& pickupController.heldObject.GetComponent<lingjiandate>()!=null)//��װС������
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
        lingjiandate lingjiandate = car.GetComponent<lingjiandate>();
        car.transform.parent = null;
        car.transform.position = spawnPosition;
        car.transform.rotation = spawnRotation;
        if (Input.GetMouseButtonDown(0))
        {
            switch (car.name)
            {
                case "����tulun":
                    if (!istulun)
                    {
                       
                        tulun1.SetActive(true);
                        tulun2.SetActive(true);
                        tulunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate.C[3].weight;
                        Xiaochedate.totalwending += lingjiandate.C[3].wending;
                        pickupController.heldObject = null;
                        //Destroy(car);
                        istulun = true;
                    }
                    break;
                case "����tulun":
                    if (!istulun)
                    {
                        
                        tulun1.SetActive(true);
                        tulun2.SetActive(true);
                        tulunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate.C[2].weight;
                        Xiaochedate.totalwending += lingjiandate.C[2].wending;
                        pickupController.heldObject = null;
                        //Destroy(car);
                        istulun = true;
                    }
                    break;
                case "����chelun":
                    if (!ischelun)
                    {
                        
                        car.transform.rotation = spawnRotation;
                        chelun.SetActive(true);
                        ischelun = true;
                        chelunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate.C[1].weight;
                        Xiaochedate.totalwending += lingjiandate.C[1].wending;
                        pickupController.heldObject = null;
                    }
                    break;
                case "����chelun":
                    if (!ischelun)
                    {
                        
                        chelun.SetActive(true);
                        ischelun = true;
                        chelunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate.C[0].weight;
                        Xiaochedate.totalwending += lingjiandate.C[0].wending;
                        pickupController.heldObject = null;
                      
                    }
                    break;
                case "����dizuo":
                    if (!isdizuo)
                    {
                        
                        dizuo.SetActive(true);
                    isdizuo = true;
                    dizuopart.SetActive(true);
                    Xiaochedate.totalweight += lingjiandate.C[5].weight;
                    Xiaochedate.totalwending += lingjiandate.C[5].wending;
                    pickupController.heldObject = null;
                     }
                    break;
                case "����dizuo":
                    if (!isdizuo)
                    {
                        
                        dizuo.SetActive(true);
                        isdizuo = true;
                        dizuopart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate.C[4].weight;
                        Xiaochedate.totalwending += lingjiandate.C[4].wending;
                        pickupController.heldObject = null;
                       
                    }
                    break;
            }
            car.SetActive(false);
            Debug.Log("��ƴװ: " + car.name);
           
        }
    }
    public void Disassemblychelun()
    {
        if (ischelun)
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("chelun"));
            lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();
            foundObject.SetActive(true);
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                if (foundObject.name == lingjiandate.C[i].gameobjectname)
                {
                    Xiaochedate.totalweight -= lingjiandate.C[i].weight;
                    Xiaochedate.totalwending -= lingjiandate.C[i].wending;
                    break;
                }
            }
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
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("tulun"));
            if (foundObject != null)
            {
                foundObject.SetActive(true);
            }
            else
                Debug.LogError("δ�ҵ�ƥ��Ķ���");
            lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                if (foundObject.name == lingjiandate.C[i].gameobjectname)
                {
                    Xiaochedate.totalweight -= lingjiandate.C[i].weight;
                    Xiaochedate.totalwending -= lingjiandate.C[i].wending;
                    break;
                }
            }
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
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name .Contains("dizuo"));
            foundObject.SetActive(true);
            lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();
            int i = 0;
            for(i=0;i<6;i++)
            {
                if (foundObject.name == lingjiandate.C[i].gameobjectname )
                {
                    Xiaochedate.totalweight -= lingjiandate.C[i].weight;
                    Xiaochedate.totalwending -= lingjiandate.C[i].wending;
                    break;
                }
            }
            dizuo.SetActive(false);
            HideGameObjects.Remove(foundObject);
        }
        else
        {

        }
        isdizuo = false;
    }
}