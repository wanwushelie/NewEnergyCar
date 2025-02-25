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

        if (uiPrefab != null)
        {
            currentUI = Instantiate(uiPrefab);
            currentUI.transform.SetParent(uiCanvasPosition, false);
            currentUI.transform.localPosition = Vector3.zero;
            currentUI.SetActive(true);
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