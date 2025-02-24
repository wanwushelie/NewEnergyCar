using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour
{
    public GameObject pickupUI; // 拾取UI预制体
    private Canvas canvas;
    private Button pickupButton;
    private bool isBeingPickedUp = false;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>(); // 获取场景中的Canvas
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene.");
            return;
        }

        Instantiate(pickupUI, canvas.transform); // 实例化UI预制体并附加到Canvas下
        pickupButton = pickupUI.GetComponentInChildren<Button>();
        pickupButton.onClick.AddListener(OnPickupButtonClick);
        pickupUI.SetActive(false); // 初始状态下隐藏UI
    }

    void Update()
    {
        if (!isBeingPickedUp)
        {
            CheckForMouseClick();
        }
    }

    void CheckForMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ShowPickupUI();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 检测是否与玩家碰撞
        {
            ShowPickupUI();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 检测是否离开玩家
        {
            HidePickupUI();
        }
    }

    void ShowPickupUI()
    {
        pickupUI.SetActive(true); // 显示UI
    }

    void HidePickupUI()
    {
        pickupUI.SetActive(false); // 隐藏UI
    }

    void OnPickupButtonClick()
    {
        // 发送事件或调用函数通知角色进行拾取
        PickupController pickupController = FindObjectOfType<PickupController>();
        if (pickupController != null)
        {
            pickupController.Pickup(this.gameObject);
            isBeingPickedUp = true;
        }
    }
}



