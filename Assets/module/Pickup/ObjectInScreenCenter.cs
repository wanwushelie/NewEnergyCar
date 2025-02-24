using UnityEngine;
using UnityEngine.UI;

public class ObjectInScreenCenter : MonoBehaviour
{
    public RectTransform uiPanel; // UI Panel to show when object is in center
    public Transform referenceObject; // Reference object to set the position of this object
    private Camera mainCamera; // Main camera used for screen space conversion

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        if (IsObjectInCenter(screenPosition))
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }

        // Check if "Q" key is pressed and referenceObject is assigned
        if (Input.GetKeyDown(KeyCode.Q) && referenceObject != null)
        {
            AdjustPositionAndParent();
        }

        // Check if "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            ResetPositionAndParent();
        }
    }

    bool IsObjectInCenter(Vector3 screenPosition)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Define a tolerance area around the center of the screen
        float centerX = screenWidth / 2f;
        float centerY = screenHeight / 2f;
        float tolerance = 50f; // Adjust this value as needed

        return Mathf.Abs(screenPosition.x - centerX) <= tolerance && Mathf.Abs(screenPosition.y - centerY) <= tolerance;
    }

    void ShowUI()
    {
        if (uiPanel != null)
        {
            uiPanel.gameObject.SetActive(true);
        }
    }

    void HideUI()
    {
        if (uiPanel != null)
        {
            uiPanel.gameObject.SetActive(false);
        }
    }

    void AdjustPositionAndParent()
    {
        // Set the position of this object to match the reference object's position
        transform.position = referenceObject.position;

        // Make this object a child of the reference object
        transform.SetParent(referenceObject, true);
    }

    void ResetPositionAndParent()
    {
        // Remove the object from its current parent without changing its world position
        transform.SetParent(null, true);
    }
}



