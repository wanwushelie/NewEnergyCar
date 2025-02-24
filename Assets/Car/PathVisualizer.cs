using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    public Color lineColor = Color.red;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        for (int i = 0; i < positions.Length - 1; i++)
        {
            Gizmos.DrawLine(positions[i], positions[i + 1]);
        }
    }
}