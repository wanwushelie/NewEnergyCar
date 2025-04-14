using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RETURN : MonoBehaviour
{
    public GameObject main, xiaoche, xiaochecamera;
    public Assemble assemble;
    public CarController carController;
    void Start()
    {

    }

    public void returnorigin()
        {
        main.SetActive(true);
        xiaochecamera.SetActive(false);
        xiaoche.transform.position = carController.waypoints[0].position;
        carController.currentWaypointIndex = 0;
        assemble.haveassmble = false;
        assemble.Disassemblychelun();
        assemble.Disassemblytulun();
        assemble.Disassemblydizuo();
        assemble.pickupmethod1.ischelun = false;
        assemble.pickupmethod1.istulun = false;
        assemble.pickupmethod1.isdizuo = false;
    }//仅做调用

 
}
