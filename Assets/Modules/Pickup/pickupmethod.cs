using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class pickupmethod : MonoBehaviour
{
       // 拾取UI预制体
    public PickupController pickupController; // 拾取控制器
    public Text pickupText;// 动态生成的UI实例
    public Button pickupButton;        // 拾取按钮组件
    public  Text objectNameText;        // UI中的文本组件
    public  GameObject targetObject;// 当前待拾取的物体
    private string pickupname;
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
    private GameObject hitObject;
    public GameObject panelpick,pu,pd;//拾取面板、拾取题词、放下题词
    private Camera mainCamera;
    void Start()
    {
        //InitializePickupUI();
        mainCamera = Camera.main;

}

    void Update()
    {
        if (Input.mousePosition != Vector3.zero)
            CheckForClickableObject();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

   
    void CheckForClickableObject()
    {
        if (EventSystem.current.IsPointerOverGameObject() && !pickupController.IsHoldingObject) return;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       // RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("11111111111");
            hitObject = hit.collider.gameObject;
            pickupname = hitObject.name;
            pickupText.text = pickupname.ToString();
            // 如果点击的是可拾取物体且当前未持有物体
            if (IsPickupable(hitObject) && !pickupController.IsHoldingObject)
            {
                targetObject = hitObject;
                spawnPosition = hitObject.transform.position;
                spawnRotation = hitObject.transform.rotation;//储存拾取物体位置
                //hideobject = Instantiate(hitObject, spawnPosition, spawnRotation);
                //hideobject.SetActive(false);
                panelpick.SetActive(true);
                pu.SetActive(true);
                pd.SetActive(false);
                if(Input.GetKeyDown(KeyCode.X))
                {
                    OnPickupX();
                    HideGameObjects.Add(hitObject);
                }
                //ShowPickupUI(hitObject.name);
            }
            else
            {
                ClearTargetAndHideUI();
            }
           
            if (pickupController.IsHoldingObject && hit.collider.CompareTag("xiaoche"))
            {
                panelpick.SetActive(true);
                pickupText.text = pickupController.heldObject.name.ToString();
                pu.SetActive(false);
                pd.SetActive(true);
                if (Input.GetKeyDown(KeyCode.X))
                {
                    pickupController.PutDown();
                }

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
    public void OnPickupX()
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
        panelpick.SetActive(false);
        pickupText.text = "";
        //pickupUI.SetActive(false);
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
