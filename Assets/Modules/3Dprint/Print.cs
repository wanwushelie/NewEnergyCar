using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Print : MonoBehaviour
{
    public GameObject banzi;
    public Transform trans;
    private float fadeSpeed = 0.005f; // �����ٶ�  
    public float startHeight;// ��ʼ�߶�  
    public float targetHeight; // Ŀ��߶�  
    public  Renderer rend;
    private Color originalColor;
    private bool isprint = false;
    private bool isstart = false;
    float timer = 0;
    public CharacterController playercontrol;
    public ThirdPersonController ThirdPersonController;
    public GameObject Camera1;
    public GameObject Camera2;
    private float currentHeight;
    void Start()
    {

        trans = GetComponent<Transform>();
         
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isprint)
        {
            currentHeight = transform.position.y;
            if (isstart)
            { 
                startHeight = trans.position.y ; // �±���  
                targetHeight = trans.position.y+ (rend.bounds.size.y); // �ϱ��� 
                isstart = false;
            }
            // ������廹δ����Ŀ��߶ȣ�������ƶ�  
            if (currentHeight < targetHeight)
            {
                currentHeight += fadeSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            }
            else if (currentHeight >= targetHeight)
            {
                timer += Time.deltaTime;
                if (timer >= 3)
                {
                    playercontrol.enabled = true;
                    ThirdPersonController.enabled = true;
                    Camera1.SetActive(true);
                    Camera2.SetActive(false);
                    isprint = false;
                }
            }
        
            // ����͸���ȣ����ݸ߶ȱ���  
            float alpha = Mathf.Clamp01((currentHeight - startHeight) / (targetHeight - startHeight));
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
    }
    public void buttomclick()
    {
        transform.position = new Vector3(banzi.transform.position.x, banzi.transform.position.y ,banzi.transform.position.z);
        isprint = true;
        isstart = true;
        Camera1.SetActive(false);
        Camera2.SetActive(true);
    }
}
