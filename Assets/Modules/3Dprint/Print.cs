using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Print : MonoBehaviour
{
    public printdata printdata1;
    public GameObject banzi,dizuo,chelun,tulun,printpanel,printing;
    //public Transform trans;
    private float fadeSpeed = 0.005f; // 渐显速度  
    public float startHeight;// 开始高度  
    public float targetHeight; // 目标高度  
    public  Renderer rend;
    private Color originalColor;
    public bool isprint = false;
    public bool isstart = false;
    float timer = 0;
    //public CharacterController playercontrol;
    //public ThirdPersonController ThirdPersonController;
    public GameObject Camera1;
    public GameObject Camera2;
    private float currentHeight;
    void Start()
    {

        //trans = GetComponent<Transform>();
         
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isprint)
        {
            currentHeight = printing.transform.position.y;
            if (isstart)
            { 
                startHeight = printing.transform.position.y ; // 下表面  
                targetHeight = printing.transform.position.y+ (rend.bounds.size.y); // 上表面 
                isstart = false;
            }
            // 如果物体还未到达目标高度，则继续移动  
            if (currentHeight < targetHeight)
            {
                currentHeight += fadeSpeed * Time.deltaTime;
                printing.transform.position = new Vector3(printing.transform.position.x, currentHeight, printing.transform.position.z);
            }
            else if (currentHeight >= targetHeight)
            {
                timer += Time.deltaTime;
                if (timer >= 3)
                {
                    //playercontrol.enabled = true;
                    //ThirdPersonController.enabled = true;
                    Camera1.SetActive(true);
                    Camera2.SetActive(false);
                    isprint = false;
                }
            }
        
            // 计算透明度，根据高度比例  
            float alpha = Mathf.Clamp01((currentHeight - startHeight) / (targetHeight - startHeight));
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
    }
    public void itemswitch()
    {
        switch (this.name)
        {
            case "塑料底座":
                printdata1.putdizuo();
                break;
            case "塑料凸轮":
                printdata1.puttulun();
                break;
            case "塑料车轮":
                printdata1.putchelun();
                break;
        }
    }
    public void buttomclick()
    {
        if (printdata1.isdizuo || printdata1.ischelun || printdata1.istulun)
        {
            if (printdata1.isdizuo)
            {
                dizuo.transform.position = new Vector3(banzi.transform.position.x, banzi.transform.position.y, banzi.transform.position.z);
                printing = dizuo;
            }
            if (printdata1.ischelun)
            {
                chelun.transform.position = new Vector3(banzi.transform.position.x, banzi.transform.position.y, banzi.transform.position.z);
                printing = chelun;
            }
            if (printdata1.istulun)
            {
                tulun.transform.position = new Vector3(banzi.transform.position.x, banzi.transform.position.y, banzi.transform.position.z);
                printing = tulun;
            }
            isprint = true;
            isstart = true;
            Camera1.SetActive(false);
            Camera2.SetActive(true);
            printpanel.SetActive(false);
            printdata1.renew();
        }
    }
}
