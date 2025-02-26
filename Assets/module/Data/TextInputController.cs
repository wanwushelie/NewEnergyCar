using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // 引入字典所需的命名空间

public class TextInputController : MonoBehaviour
{
    public InputField inputField; // 绑定输入框
    public Text feedbackText;    // 用于显示反馈信息

    private void Start()
    {
        if (inputField == null)
        {
            Debug.LogError("InputField is not assigned!");
        }

        // 预设输入内容
        inputField.text = "Cube:canbehighlighted=true";

        // 初始化快捷键映射
        InitializeShortcuts();
    }

    // 快捷键映射字典
    private Dictionary<string, string> shortcuts = new Dictionary<string, string>();

    // 初始化快捷键映射
    private void InitializeShortcuts()
    {
        // 添加快捷键映射
        shortcuts.Add("1", "Cube:canbehighlighted=true");
        shortcuts.Add("2", "Cube:canbehighlighted=false");
        shortcuts.Add("3", "Cube:showui=true");
        shortcuts.Add("4", "Cube:showui=false");
        shortcuts.Add("5", "Cube:highlightcolor=#ff0000");
        shortcuts.Add("6", "Cube:highlightcolor=#00ff00");
        shortcuts.Add("7", "Cube:highlightcolor=#0000ff");
        shortcuts.Add("8", "Cube:highlightcolor=#000000");
        shortcuts.Add("9", "Cube:highlightcolor=#ffffff");

        shortcuts.Add("0", "Cube:canbepickedup=true");
        shortcuts.Add("00", "Cube:canbepickedup=false");
        // 可以继续添加更多快捷键映射
    }

    // 调用此方法处理输入内容
    public void ProcessInput()
    {
        string input = inputField.text;

        // 检查是否为快捷键输入
        if (shortcuts.ContainsKey(input))
        {
            input = shortcuts[input]; // 替换为对应的完整指令
        }

        if (string.IsNullOrEmpty(input))
        {
            feedbackText.text = "输入不能为空！";
            return;
        }

        // 解析输入指令
        string[] parts = input.Split(':');
        if (parts.Length != 2)
        {
            feedbackText.text = "格式错误！请使用 '物体名称:属性=值' 的格式。";
            return;
        }

        string objectName = parts[0].Trim();
        string[] propertyParts = parts[1].Split('=');
        if (propertyParts.Length != 2)
        {
            feedbackText.text = "格式错误！请使用 '属性=值' 的格式。";
            return;
        }

        string propertyName = propertyParts[0].Trim().ToLower();
        string value = propertyParts[1].Trim();

        // 根据输入修改物体数据
        ObjectData data = ObjectDataManager.Instance.GetData(objectName);
        if (data == null)
        {
            feedbackText.text = $"未找到名为 '{objectName}' 的物体数据！";
            return;
        }

        bool success = ModifyObjectData(data, propertyName, value);
        if (success)
        {
            feedbackText.text = $"成功修改 '{objectName}' 的 {propertyName} 为 {value}。";
        }
        else
        {
            feedbackText.text = $"无法修改属性 '{propertyName}'。";
        }

        inputField.text = ""; // 清空输入框
    }

    // 修改物体数据的具体实现
    private bool ModifyObjectData(ObjectData data, string propertyName, string value)
    {
        bool success = false;

        switch (propertyName)
        {
            case "canbehighlighted":
                bool highlightValue;
                if (bool.TryParse(value, out highlightValue))
                {
                    data.canBeHighlighted = highlightValue;
                    success = true;
                }
                break;

            case "highlightcolor":
                Color colorValue;
                if (ColorUtility.TryParseHtmlString(value, out colorValue))
                {
                    data.highlightColor = colorValue;
                    success = true;
                }
                break;

            case "showui":
                bool showUIValue;
                if (bool.TryParse(value, out showUIValue))
                {
                    data.showUI = showUIValue;
                    success = true;
                }
                break;

            case "canbepickedup":
                bool pickupValue;
                if (bool.TryParse(value, out pickupValue))
                {
                    data.canBePickedUp = pickupValue;
                    success = true;
                }
                break;

            default:
                Debug.LogWarning($"未知属性 '{propertyName}'。");
                break;
        }

        return success;
    }
}