using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Print : MonoBehaviour
{
    public GameObject Cube, Sphere,banzi;
    private float fadeSpeed = 0.02f; // �����ٶ�  
    private float startHeight;// ��ʼ�߶�  
    private float targetHeight; // Ŀ��߶�  
    private Renderer rend;
    private Color originalColor;
    private bool isprint = false;
    void Start()
    {

        rend = GetComponent<Renderer>();
        startHeight = transform.position.y - (rend.bounds.size.y ); // �±���  
        targetHeight = transform.position.y ; // �ϱ���  
        originalColor = rend.material.color;
        transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isprint)
        {
            float currentHeight = transform.position.y;

            // ������廹δ����Ŀ��߶ȣ�������ƶ�  
            if (currentHeight < targetHeight)
            {
                currentHeight += fadeSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            }

            // ����͸���ȣ����ݸ߶ȱ���  
            float alpha = Mathf.Clamp01((currentHeight - startHeight) / (targetHeight - startHeight));
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
    }
    public void buttomclick()
    {
        isprint = true;
        this.transform.position = new Vector3(banzi.transform.position.x, banzi.transform.position.y- rend.bounds.size.y/2, banzi.transform.position.z);
    }
}
