using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject interactivepanel;
    public GameObject diannaopanel, qiegejipanel, dayinjipanel;
    public CharacterController playercontrol;
    public ThirdPersonController ThirdPersonController;
    private Text interact;
    public ObjectDataManager ObjectDataManager;
    private ObjectData objectData;
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
        interact.text = "��F ʹ��" + this.name;
        objectData = ObjectDataManager.Instance.GetData(this.name);
        if (objectData.objectName=="��ӡ��" && Input.GetKeyDown(KeyCode.F))
        {
            dayinjipanel.SetActive(true);
            interactivepanel.SetActive(false);
            playercontrol.enabled = false;
            ThirdPersonController.enabled = false;
        }
        if (objectData.objectName == "�и��" && Input.GetKeyDown(KeyCode.F))
        {
            qiegejipanel.SetActive(true);
            interactivepanel.SetActive(false);
            playercontrol.enabled = false;
            ThirdPersonController.enabled = false;
        }
        if (objectData.objectName == "��������" && Input.GetKeyDown(KeyCode.F))
        {
            diannaopanel.SetActive(true);
            interactivepanel.SetActive(false);
            playercontrol.enabled = false;
            ThirdPersonController.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactivepanel.SetActive(false);
    }

}



