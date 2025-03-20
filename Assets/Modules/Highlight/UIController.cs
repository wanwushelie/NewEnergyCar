using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    public Transform uiCanvasPosition; // UI显示的Canvas定位点
    private GameObject currentUI; // 当前显示的UI

    private void Awake()
    {
        Instance = this;
    }

    public void ShowUI(GameObject uiPrefab)
    {
        if (currentUI != null)
        {
            Destroy(currentUI); // 销毁当前显示的UI
        }

        if (uiPrefab != null) // 检查传入的UI预制体是否不为空
        {
            currentUI = Instantiate(uiPrefab); // 实例化传入的UI预制体
            currentUI.transform.SetParent(uiCanvasPosition, false); // 将实例化的UI设置为uiCanvasPosition的子对象
            currentUI.transform.localPosition = Vector3.zero; // 将实例化的UI的本地位置设置为原点
            currentUI.SetActive(true); // 激活实例化的UI
        }
    }

    public void HideUI()
    {
        if (currentUI != null)
        {
            Destroy(currentUI); // 销毁当前显示的UI
            currentUI = null;
        }
    }
}