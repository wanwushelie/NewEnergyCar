using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out localPoint))
            {
                // 计算当前位置相对于初始位置的偏移量
                Vector2 offset = localPoint - (Vector2)rectTransform.anchoredPosition;
                rectTransform.anchoredPosition += offset;
            }
        }
    }

}