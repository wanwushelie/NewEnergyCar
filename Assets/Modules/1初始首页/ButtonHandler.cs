using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button[] buttons;
    public string[] sceneNames; // 使用字符串数组来存储场景名称

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => LoadScene(sceneNames[index])); // 使用场景名称
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit(); // 只在构建的游戏版本中起作用
    }
}