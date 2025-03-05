using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class pickupmethod : MonoBehaviour
{
    public GameObject pickupUIPrefab;   // 拾取UI预制体
    public PickupController pickupController; // 拾取控制器

    private GameObject pickupUI;        // 动态生成的UI实例
    private Button pickupButton;        // 拾取按钮组件
    private Text objectNameText;        // UI中的文本组件
    private GameObject targetObject;// 当前待拾取的物体
    public string pickupname;
    public GameObject dizuo, chelun, tulun1, tulun2;
    Vector3 spawnPosition; // 拾取物品的位置  
    Quaternion spawnRotation = Quaternion.identity; // 默认为无旋转
    public GameObject hideobject;
    public GameObject chaixieUI, chelunpart, tulunpart, dizuopart;//显示ui
    public bool ischelun = false, isdizuo = false, istulun = false;//判断小车上是否搭载组件
    public List<GameObject> HideGameObjects = new List<GameObject>();
    public xiaochedate Xiaochedate;
    private ObjectData objectData;
    public ObjectDataManager objectdatamanager;
    public lingjiandate lingjiandate1;

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
    }

    // 初始化拾取UI
    void InitializePickupUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("场景中未找到Canvas！");
            return;
        }

        // 实例化UI并获取组件
        pickupUI = Instantiate(pickupUIPrefab, canvas.transform);
        pickupButton = pickupUI.GetComponentInChildren<Button>();
        objectNameText = pickupUI.GetComponentInChildren<Text>();
        pickupUI.SetActive(false);

        // 绑定按钮事件
        if (pickupButton != null)
        {
            pickupButton = pickupUI.GetComponentInChildren<Button>();
            pickupButton.onClick.AddListener(OnPickupButtonClicked);
        }
        else
        {
            Debug.LogError("拾取UI中未找到Button组件！");
        }
    }

    // 检测鼠标点击的物体
    void CheckForClickableObject()
    {
        if (EventSystem.current.IsPointerOverGameObject() && !pickupController.IsHoldingObject) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // 如果点击的是可拾取物体且当前未持有物体
            if (IsPickupable(hitObject) && !pickupController.IsHoldingObject)
            {
                targetObject = hitObject;
                spawnPosition = hitObject.transform.position;
                spawnRotation = hitObject.transform.rotation;//储存拾取物体位置
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
            if (pickupController.IsHoldingObject && hit.collider.CompareTag("xiaoche"))//组装小车部件
            {
                pickupController.PutDown();

            }
            if (pickupController.IsHoldingObject && hitObject.name == "机械小车未完成" && ObjectDataManager.Instance.GetData(pickupController.heldObject.name).canBemakeup)//组装小车部件
            {
                Assemble(pickupController.heldObject);

            }
            else if (!pickupController.IsHoldingObject && hitObject.name == "机械小车未完成")//拆卸小车部件
            {
                chaixieUI.SetActive(true);
            }

        }
        else
        {
            ClearTargetAndHideUI();
        }
    }

    // 判断物体是否可拾取
    bool IsPickupable(GameObject obj)
    {
        objectData = ObjectDataManager.Instance.GetData(obj.name);
        if (objectData == null)
        {
            Debug.LogWarning("未找到物体数据: " + obj.name);
            return false; // 如果没有找到数据，直接返回不可拾取  
        }
        if (objectData.canBePickedUp)
        {
            return true;
        }
        return false;
    }

    // 显示拾取UI
    void ShowPickupUI(string objectName)
    {
        objectNameText.text = "拾取: " + objectName;
        pickupUI.SetActive(true);
    }

    // 点击拾取按钮时的逻辑
    public void OnPickupButtonClicked()
    {
        if (targetObject != null)
        {
            pickupController.Pickup(targetObject);
            ClearTargetAndHideUI();
        }
        Debug.Log("按钮被点击！");
    }

    // 清空目标并隐藏UI
    void ClearTargetAndHideUI()
    {
        targetObject = null;
        pickupUI.SetActive(false);
    }
  
    void Assemble(GameObject car)
    {
       
        car.transform.parent = null;
        car.transform.position = spawnPosition;
        car.transform.rotation = spawnRotation;
        if (Input.GetMouseButtonDown(0))
        {
            switch (car.name)
            {
                case "金属凸轮":
                    if (!istulun)
                    {

                        tulun1.SetActive(true);
                        tulun2.SetActive(true);
                        tulunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate1.C[3].weight;
                        Xiaochedate.totalwending += lingjiandate1.C[3].wending;
                        //Destroy(car);
                        istulun = true;
                        car.SetActive(false);
                        pickupController.heldObject = null;
                    }
                    break;
                case "塑料凸轮":
                    if (!istulun)
                    {

                        tulun1.SetActive(true);
                        tulun2.SetActive(true);
                        tulunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate1.C[2].weight;
                        Xiaochedate.totalwending += lingjiandate1.C[2].wending;
                        pickupController.heldObject = null;
                        //Destroy(car);
                        istulun = true;
                        car.SetActive(false);
                    }
                    break;
                case "金属车轮":
                    if (!ischelun)
                    {

                        car.transform.rotation = spawnRotation;
                        chelun.SetActive(true);
                        ischelun = true;
                        chelunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate1.C[1].weight;
                        Xiaochedate.totalwending += lingjiandate1.C[1].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
                case "塑料车轮":
                    if (!ischelun)
                    {

                        chelun.SetActive(true);
                        ischelun = true;
                        chelunpart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate1.C[0].weight;
                        Xiaochedate.totalwending += lingjiandate1.C[0].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
                case "金属底座":
                    if (!isdizuo)
                    {

                        dizuo.SetActive(true);
                        isdizuo = true;
                        dizuopart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate1.C[5].weight;
                        Xiaochedate.totalwending += lingjiandate1.C[5].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
                case "塑料底座":
                    if (!isdizuo)
                    {

                        dizuo.SetActive(true);
                        isdizuo = true;
                        dizuopart.SetActive(true);
                        Xiaochedate.totalweight += lingjiandate1.C[4].weight;
                        Xiaochedate.totalwending += lingjiandate1.C[4].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
            }
            pickupController.heldObject = null;
            Debug.Log("已拼装: " + car.name);

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
                Debug.LogError("未找到匹配的对象！");
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
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("dizuo"));
            foundObject.SetActive(true);
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
            dizuo.SetActive(false);
            HideGameObjects.Remove(foundObject);
        }
        else
        {

        }
        isdizuo = false;
    }
}
