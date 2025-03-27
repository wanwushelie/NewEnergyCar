using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printdata : MonoBehaviour
{
    public bool isdizuo = false, ischelun = false, istulun = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
