using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject interactivepanel;
    public GameObject diannaopanel,printpanel;
    //public CharacterController playercontrol;
    //public ThirdPersonController ThirdPersonController;
    private Text interact;
    public ObjectDataManager ObjectDataManager;
    private ObjectData objectData;
    public GameObject main, qiege;
    public PolygonDrawer PolygonDrawer1;
    public bool isinter=false;
    void Start()
    {
        interact = interactivepanel.GetComponentInChildren<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        interactivepanel.SetActive(!isinter);
        interact.text = "按F使用" + this.name;
        objectData = ObjectDataManager.Instance.GetData(this.name);
        if (objectData.objectName == "主机电脑3d打印" && Input.GetKeyDown(KeyCode.F))
        {
            printpanel.SetActive(true);
            interactivepanel.SetActive(false);
            isinter = true;
            //playercontrol.enabled = false;
            //ThirdPersonController.enabled = false;
        }
        else if(objectData.objectName == "主机电脑切割" && Input.GetKeyDown(KeyCode.F))
        {
            diannaopanel.SetActive(true);
            interactivepanel.SetActive(false);
            isinter = true;
            PolygonDrawer1.zhezhao.SetActive(true);
            //playercontrol.enabled = false;
            //ThirdPersonController.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactivepanel.SetActive(false);
        isinter = false;
    }
    public void click()
    {
        PolygonDrawer1.enabled = true;
        qiege.SetActive(true); ;
        main.SetActive(false);
        diannaopanel.SetActive(false);
    }

}



