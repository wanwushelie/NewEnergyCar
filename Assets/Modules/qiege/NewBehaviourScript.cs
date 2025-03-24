using System;
using System.Collections.Generic;
using UnityEngine;

public class MeshBoolean
{
    // 构造函数
    public MeshBoolean() { }

    // 切割Mesh
    public Mesh SliceMesh(Mesh originalMesh, Vector3[] polygon)
    {
        // 将多边形转换为平面
        Plane plane = GetPlaneFromPolygon(polygon);

        // 分割Mesh
        List<Vector3> positiveVertices = new List<Vector3>();
        List<int> positiveTriangles = new List<int>();
        List<Vector3> negativeVertices = new List<Vector3>();
        List<int> negativeTriangles = new List<int>();

        // 遍历原始Mesh的三角形
        for (int i = 0; i < originalMesh.triangles.Length; i += 3)
        {
            Vector3 v1 = originalMesh.vertices[originalMesh.triangles[i]];
            Vector3 v2 = originalMesh.vertices[originalMesh.triangles[i + 1]];
            Vector3 v3 = originalMesh.vertices[originalMesh.triangles[i + 2]];

            // 检测三角形与平面的交点
            float[] distances = new float[] { plane.GetDistanceToPoint(v1), plane.GetDistanceToPoint(v2), plane.GetDistanceToPoint(v3) };

            // 判断三角形与平面的关系
            if (distances[0] >= 0 && distances[1] >= 0 && distances[2] >= 0)
            {
                // 三角形完全在平面的正侧
                positiveVertices.AddRange(new Vector3[] { v1, v2, v3 });
                positiveTriangles.AddRange(new int[] { positiveVertices.Count - 3, positiveVertices.Count - 2, positiveVertices.Count - 1 });
            }
            else if (distances[0] < 0 && distances[1] < 0 && distances[2] < 0)
            {
                // 三角形完全在平面的负侧
                negativeVertices.AddRange(new Vector3[] { v1, v2, v3 });
                negativeTriangles.AddRange(new int[] { negativeVertices.Count - 3, negativeVertices.Count - 2, negativeVertices.Count - 1 });
            }
            else
            {
                // 三角形与平面相交，需要分割
                Vector3[] intersectionPoints = new Vector3[3];
                int intersectionCount = 0;

                if (distances[0] * distances[1] < 0)
                {
                    intersectionPoints[intersectionCount++] = GetIntersectionPoint(v1, v2, plane);
                }
                if (distances[1] * distances[2] < 0)
                {
                    intersectionPoints[intersectionCount++] = GetIntersectionPoint(v2, v3, plane);
                }
                if (distances[2] * distances[0] < 0)
                {
                    intersectionPoints[intersectionCount++] = GetIntersectionPoint(v3, v1, plane);
                }

                // 根据交点生成新的三角形
                if (intersectionCount == 2)
                {
                    Vector3 v4 = intersectionPoints[0];
                    Vector3 v5 = intersectionPoints[1];

                    positiveVertices.AddRange(new Vector3[] { v1, v4, v5 });
                    positiveTriangles.AddRange(new int[] { positiveVertices.Count - 3, positiveVertices.Count - 2, positiveVertices.Count - 1 });

                    negativeVertices.AddRange(new Vector3[] { v4, v5, v2 });
                    negativeTriangles.AddRange(new int[] { negativeVertices.Count - 3, negativeVertices.Count - 2, negativeVertices.Count - 1 });
                }
            }
        }

        // 创建新的Mesh
        Mesh positiveMesh = new Mesh();
        positiveMesh.vertices = positiveVertices.ToArray();
        positiveMesh.triangles = positiveTriangles.ToArray();
        positiveMesh.RecalculateNormals();

        return positiveMesh;
    }

    // 从多边形获取平面
    private Plane GetPlaneFromPolygon(Vector3[] polygon)
    {
        Vector3 normal = Vector3.Cross(polygon[1] - polygon[0], polygon[2] - polygon[1]).normalized;
        return new Plane(normal, polygon[0]);
    }

    // 获取线段与平面的交点
    private Vector3 GetIntersectionPoint(Vector3 p1, Vector3 p2, Plane plane)
    {
        float enter;
        Ray ray = new Ray(p1, p2 - p1);
        plane.Raycast(ray, out enter);
        return ray.GetPoint(enter);
    }
}