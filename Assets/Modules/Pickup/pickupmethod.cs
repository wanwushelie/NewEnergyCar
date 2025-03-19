using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class pickupmethod : MonoBehaviour
{
    // 拾取UI预制体
    public GameObject targetObject;// 当前待拾取的物体
    public GameObject hideobject;
    private GameObject hitObject;
    public GameObject panelpick, pu, pd, sh;//拾取面板、拾取题词、放下题词、使用题词
    public PickupController pickupController; // 拾取控制器
    public Text pickupText;// 动态生成的UI实例
    public Button pickupButton;        // 拾取按钮组件
    public Text objectNameText;        // UI中的文本组件
    private string pickupname;
    public Vector3 spawnPosition; // 拾取物品的位置  
    public Quaternion spawnRotation = Quaternion.identity; // 默认为无旋转
    public bool ischelun = false, isdizuo = false, istulun = false;//判断小车上是否搭载组件
    public xiaochedate Xiaochedate;
    private ObjectData objectData;
    public ObjectDataManager objectdatamanager;
    public lingjiandate lingjiandate1;
    public lingjianused lingjianused1;
    public Assemble assemble1;

    private Camera mainCamera;
    void Start()
    {
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
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.collider.gameObject;
            pickupname = hitObject.name;
            pickupText.text = pickupname.ToString();
            // 如果点击的是可拾取物体且当前未持有物体
            if (IsPickupable(hitObject) && !pickupController.IsHoldingObject)
            {
                objectData = ObjectDataManager.Instance.GetData(hitObject.name);
                panelpick.SetActive(true);
                if (objectData.isshowed)
                {
                    pu.SetActive(false);
                    pd.SetActive(false);
                    sh.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        lingjianused1.showed(hitObject);
                    }
                }
                else
                {
                    targetObject = hitObject;
                    spawnPosition = hitObject.transform.position;
                    spawnRotation = hitObject.transform.rotation;//储存拾取物体位置
                    pu.SetActive(true);
                    pd.SetActive(false);
                    sh.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        OnPickupX();
                        assemble1.HideGameObjects.Add(hitObject);
                    }
                }
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
                sh.SetActive(false);
                if (Input.GetKeyDown(KeyCode.X))
                {
                    pickupController.PutDown();
                }

            }
            if (pickupController.IsHoldingObject && hitObject.name == "机械小车未完成" && ObjectDataManager.Instance.GetData(pickupController.heldObject.name).canBemakeup)//组装小车部件
            {
                if(Input.GetKeyDown(KeyCode.X))
                assemble1.assemble(pickupController.heldObject);
            }
            else if (!pickupController.IsHoldingObject && hitObject.name == "机械小车未完成"&& Input.GetKeyDown(KeyCode.X))//拆卸小车部件
            {
                assemble1.chaixieUI.SetActive(true);
            }

        }
        else
        {
            ClearTargetAndHideUI();
        }
    }
    bool IsPickupable(GameObject obj)
    {
        objectData = ObjectDataManager.Instance.GetData(obj.name);
        if (objectData == null)
        {
            return false; // 如果没有找到数据，直接返回不可拾取  
        }
        if (objectData.canBePickedUp)
        {
            return true;
        }
        return false;
    }//判断是否可拾取

    public void OnPickupX()
    {
        if (targetObject != null)
        {
            pickupController.Pickup(targetObject);
            ClearTargetAndHideUI();
        }
    }
    // 清空目标并隐藏UI
    void ClearTargetAndHideUI()
    {
        targetObject = null;
        panelpick.SetActive(false);
        pickupText.text = "";
    }
}
  
   
   