using UnityEngine;

public class YourController : MonoBehaviour
{
    public HighlightableObject highlightableObject;

    void Start()
    {
        highlightableObject.Off();  // 禁用高亮
    }
}