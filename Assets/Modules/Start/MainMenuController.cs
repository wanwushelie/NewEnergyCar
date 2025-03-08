using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StarterAssets;

public class MainMenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // UI元素引用
    public GameObject startUI; // 开始界面的UI对象
    public GameObject gameUI;  // 游戏中的UI对象
    public Button startButton; // 开始游戏按钮
    public Button exitButton;  // 退出游戏按钮
    public Button settingsButton; // 设置按钮

    // 高亮效果相关
    private Image startButtonImage;
    private Image exitButtonImage;
    private Image settingsButtonImage;

    public GameObject Player; // 玩家对象

    private void Start()
    {
        // 获取按钮的Image组件
        startButtonImage = startButton.GetComponent<Image>();
        exitButtonImage = exitButton.GetComponent<Image>();
        settingsButtonImage = settingsButton.GetComponent<Image>();
        //隐藏玩家
        Player.SetActive(false);

        startUI.SetActive(true);
        gameUI.SetActive(false);
    }

    // 开始游戏按钮点击事件
    public void OnStartGame()
    {
        // 隐藏开始界面UI，显示游戏UI
        startUI.SetActive(false);
        gameUI.SetActive(true);
        // 显示玩家
        Player.SetActive(true);

        // 可以在这里添加其他游戏开始的逻辑
        Debug.Log("Game Started!");
    }

    // 退出游戏按钮点击事件
    public void OnExitGame()
    {
        Application.Quit(); // 退出应用
        Debug.Log("Game Exited!");
    }

    // 设置按钮点击事件
    public void OnSettings()
    {
        // 示例：切换设置面板的显示状态
        GameObject settingsPanel = GameObject.Find("SettingsPanel");
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }

    // 鼠标悬停时触发的高亮效果
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == startButton.gameObject)
        {
            startButtonImage.color = Color.grey; // 高亮颜色
        }
        else if (eventData.pointerCurrentRaycast.gameObject == exitButton.gameObject)
        {
            exitButtonImage.color = Color.grey;
        }
        else if (eventData.pointerCurrentRaycast.gameObject == settingsButton.gameObject)
        {
            settingsButtonImage.color = Color.grey;
        }
    }

    // 鼠标离开时恢复原状
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == startButton.gameObject)
        {
            startButtonImage.color = Color.white; // 原始颜色
        }
        else if (eventData.pointerCurrentRaycast.gameObject == exitButton.gameObject)
        {
            exitButtonImage.color = Color.white;
        }
        else if (eventData.pointerCurrentRaycast.gameObject == settingsButton.gameObject)
        {
            settingsButtonImage.color = Color.white;
        }
    }
}