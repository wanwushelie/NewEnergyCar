using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pickupmethod : MonoBehaviour
{
    public GameObject pickupUIPrefab;   // 拾取UI预制体
    public PickupController pickupController; // 拾取控制器

    private GameObject pickupUI;        // 动态生成的UI实例
    private Button pickupButton;        // 拾取按钮组件
    private Text objectNameText;        // UI中的文本组件
    private GameObject targetObject;    // 当前待拾取的物体

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

            // 获取物体的 ObjectData
            ObjectData objectData = ObjectDataManager.Instance.GetData(hitObject.name);
            if (objectData != null && objectData.canBePickedUp && !pickupController.IsHoldingObject)
            {
                targetObject = hitObject;
                ShowPickupUI(hitObject.name);
            }
            else
            {
                ClearTargetAndHideUI();
            }

            if (pickupController.IsHoldingObject && hit.collider.CompareTag("xiaoche"))
            {
                pickupController.PutDown();
            }
        }
        else
        {
            ClearTargetAndHideUI();
        }
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