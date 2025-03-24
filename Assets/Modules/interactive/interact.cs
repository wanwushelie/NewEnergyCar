using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject interactivepanel;
    public GameObject diannaopanel;
    //public CharacterController playercontrol;
    //public ThirdPersonController ThirdPersonController;
    private Text interact;
    public ObjectDataManager ObjectDataManager;
    private ObjectData objectData;
    public Camera main, qiege;
    public PolygonDrawer PolygonDrawer1;
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
        interactivepanel.SetActive(true);
        interact.text = "按F使用" + this.name;
        objectData = ObjectDataManager.Instance.GetData(this.name);
        if (objectData.objectName == "主机电脑" && Input.GetKeyDown(KeyCode.F))
        {
            diannaopanel.SetActive(true);
            interactivepanel.SetActive(false);
            //playercontrol.enabled = false;
            //ThirdPersonController.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactivepanel.SetActive(false);
    }
    public void click()
    {
        PolygonDrawer1.enabled = true;
        qiege.enabled = true;
        main.enabled = false;
        diannaopanel.SetActive(false);
    }

}



