using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using StarterAssets;

public class PolygonDrawer : MonoBehaviour
{
    public GameObject[] objects=new GameObject[3];
    public GameObject cube, polygonObject, dingmian, cylinderObject,zhezhao,qiegepanel,zhutiposition;
    public bool[] isSelected = new bool[3];
    public bool isDrawing = false, isdizuo = false, ischelun = false, istulun = false, isqiege = false, haveqiege = false;//选择打印物体并且管理总控开关
    private bool isPaused = false, chongfu = false;
    public Color fillColor = Color.red; // �����ɫ  
    public Material lineMaterial, material; // ���������Ĳ���  
    public float height = 0.2f; // ����߶�
    public PolygonDrawer PolygonDrawer1;
    private List<Vector3> points = new List<Vector3>();
    private LineRenderer lineRenderer;
    private MeshFilter meshFilter;
    public Camera main, qiege;
    public ObjectData objectDatad, objectDatac, objectDatat;
   
    void Start()
    {
       
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = fillColor;
        lineRenderer.endColor = fillColor;
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f; 
        lineRenderer.transform.position = new Vector3(0, 0, qiege.transform.position.z - 0.0000001f);
        cube.SetActive(false);
        //qiege.enabled = false;
    }

    void Update()
    {
        isSelected = new bool[] { isdizuo, ischelun, istulun };
        isqiege = (objectDatac.havebeenqiege&&!objectDatac.havebeenpicked) || (objectDatad.havebeenqiege && !objectDatad.havebeenpicked) || (objectDatat.havebeenqiege && !objectDatat.havebeenpicked);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = qiege.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Cube"))
                {
                    Debug.Log("cube");
                    isDrawing = true;
                    points.Clear();
                    points.Add(hit.point);
                    DrawLine();
                }
            }
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            Ray ray = qiege.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Cube"))
                {
                    Vector3 currentPoint = hit.point;

                    if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], currentPoint) > 0.01f)
                    {
                        points.Add(currentPoint);
                        DrawLine();  
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;
            StartCoroutine(CompleteDrawing());
        }
    }

    private void DrawLine()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray()); // ���������ĵ�  
    }

    private void CreatePolygon()
    {
        if (points.Count < 3) return; // ������Ҫ3����  

        
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[points.Count];
        int[] triangles = new int[(points.Count - 2) * 3];

        for (int i = 0; i < points.Count; i++)
        {
            vertices[i] = points[i];
        }

        for (int i = 0; i < points.Count - 2; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        
        polygonObject = new GameObject("Polygon");
        meshFilter = polygonObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = polygonObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = mesh;
        meshRenderer.material = material;
        polygonObject.transform.position = Vector3.zero;

    }
    private void Createdingmian()
    {
        if (points.Count < 3) return; 
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[points.Count];
        int[] triangles = new int[(points.Count - 2) * 3];

        for (int i = 0; i < points.Count; i++)
        {
            vertices[i] = points[i];
        }

        for (int i = 0; i < points.Count - 2; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        dingmian = new GameObject("dingmian");
        meshFilter = dingmian.AddComponent<MeshFilter>(); // ȷ��ʹ�����Ա����
        MeshRenderer meshRenderer = dingmian.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.material = material;

        Vector3 changeposition = polygonObject.transform.position;
        changeposition.y -= 0.045f;
        dingmian.transform.position = changeposition;
    }
    void GenerateCylinder(Vector3[] polygonVertices, float height)
    {
        if (polygonVertices.Length < 3)
        {
            return;
        }

        // 创建柱体对象
        cylinderObject = new GameObject("Cylinder");
        MeshFilter cylinderMeshFilter = cylinderObject.AddComponent<MeshFilter>();
        MeshRenderer cylinderMeshRenderer = cylinderObject.AddComponent<MeshRenderer>();

        // 计算底面和顶面顶点（沿Y轴方向延伸）
        Vector3[] vertices = new Vector3[polygonVertices.Length * 2];
        for (int i = 0; i < polygonVertices.Length; i++)
        {
            // 底面顶点（Y坐标保持原始位置）
            Vector3 bottomVertex = polygonVertices[i];

            // 顶面顶点（Y坐标增加高度）
            Vector3 topVertex = polygonVertices[i] + Vector3.down * height; // 修改点1：使用Y轴方向

            vertices[i] = bottomVertex;                     // 底面顶点索引：0 ~ n-1
            vertices[i + polygonVertices.Length] = topVertex; // 顶面顶点索引：n ~ 2n-1
        }

        // 生成侧面三角形（修正连接顺序）
        int triangleCount = polygonVertices.Length * 6;
        int[] triangles = new int[triangleCount];

        for (int i = 0; i < polygonVertices.Length; i++)
        {
            int next = (i + 1) % polygonVertices.Length;
            int baseIndex = i * 6;

            // 第一个三角形（底面i -> 底面next -> 顶面i）
            triangles[baseIndex] = i;
            triangles[baseIndex + 1] = next;
            triangles[baseIndex + 2] = i + polygonVertices.Length;

            // 第二个三角形（顶面i -> 底面next -> 顶面next）
            triangles[baseIndex + 3] = i + polygonVertices.Length;
            triangles[baseIndex + 4] = next;
            triangles[baseIndex + 5] = next + polygonVertices.Length;
        }

        // 生成网格
        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        cylinderMeshFilter.mesh = mesh;
        cylinderMeshRenderer.material = lineMaterial;

        // 设置父物体和位置
        cylinderObject.transform.position = Vector3.zero;
        if (polygonObject != null) polygonObject.transform.SetParent(cylinderObject.transform);
        if (dingmian != null) dingmian.transform.SetParent(cylinderObject.transform);
    }
    IEnumerator CompleteDrawing()
    {
        CreatePolygon();
        Createdingmian();

        lineRenderer.positionCount = 0;
        cube.SetActive(false);

        if (meshFilter != null && meshFilter.mesh != null)
        {
            Vector3[] polygonVertices = meshFilter.mesh.vertices;
            GenerateCylinder(polygonVertices, height);
        }
        else
        {
            Debug.LogError("MeshFilter 未正确初始化");
        }
        yield return new WaitForSeconds(3);
        int i = 0;
       
        for(i=0;i<3;i++)
        {
            if (isSelected[i])
            {
                Debug.Log("位移");
                objects[i].transform.position = zhutiposition.transform.position;
                break;
            }
        }
        istulun = isdizuo = ischelun = chongfu =false;
        main.enabled = true;
        Destroy(cylinderObject);
        qiegepanel.SetActive(false);
        haveqiege = true;
    }
    public void itemchoosed()
    {
        itemchoose(false,true,false);
    }
    public void itemchooset()
    {
        itemchoose(false,false,true);
    }
    public void itemchoosec()
    {
        itemchoose(true,false,false);
    }
    public void putbutton()
    {
        if(ischelun||isdizuo||istulun&&!isqiege&&!chongfu)
        {
            cube.SetActive(true);
            chongfu = true;
            if (ischelun&&!objectDatac.havebeenqiege)
            {
                cube.SetActive(true);
                zhezhao.SetActive(false);
                objectDatac.havebeenqiege = true;
            }
            if (istulun&&!objectDatat.havebeenqiege)
            {
                cube.SetActive(true);
                zhezhao.SetActive(false);
                objectDatat.havebeenqiege = true;
            }
            if (isdizuo&& !objectDatad.havebeenqiege)
            {
                cube.SetActive(true);
                zhezhao.SetActive(false);
                objectDatad.havebeenqiege = true;
            }
            main.enabled = false;
            qiege.enabled = true;
            
            }
    }
    public void itemchoose(bool c,bool d,bool t)
    {
        ischelun = c;
        isdizuo = d;
        istulun = t;
    }
}