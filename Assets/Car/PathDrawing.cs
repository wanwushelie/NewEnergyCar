using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    public LineRenderer path;
    public Camera mainCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                path.positionCount++;
                path.SetPosition(path.positionCount - 1, hit.point);
            }
        }
    }
}