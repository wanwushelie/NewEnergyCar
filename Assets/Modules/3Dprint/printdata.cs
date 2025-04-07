using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printdata : MonoBehaviour
{
    //public static printdata Instance { get; private set; }
    public bool isdizuo = false, ischelun = false, istulun = false, isprinting = false, haveprint = false;//记录打印状态
    public ObjectData datadizuo,datachelun,datatulun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isprinting =(datachelun.havebeenprinted&&!datachelun.havebeenpicked)|| (datatulun.havebeenprinted && !datatulun.havebeenpicked)|| (datadizuo.havebeenprinted && !datadizuo.havebeenpicked);//打印后未被拾取的物品即打印机还在工作
    }
    public void putdizuo()
    {
        isdizuo = true;
        ischelun = false;
        istulun = false;
     
    }
    public void putchelun()
    {
        isdizuo = false;
        ischelun = true;
        istulun = false;
        
    }
    public void puttulun()
    {
        isdizuo = false;
        ischelun = false;
        istulun = true;
       
    }
    public void renew()
    {
        isdizuo = false;
        ischelun = false;
        istulun = false;
       
    }
}
