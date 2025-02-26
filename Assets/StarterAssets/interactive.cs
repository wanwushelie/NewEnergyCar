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
        interact.text = "∞¥F  π”√" +this.name;
        if(this.CompareTag("dayinji")&& Input.GetKeyDown(KeyCode.F))
        {
            dayinjipanel.SetActive(true);
            interactivepanel.SetActive(false);
            playercontrol.enabled = false;
            ThirdPersonController.enabled = false;
        }
        if (this.CompareTag("qiegeji") && Input.GetKeyDown(KeyCode.F))
        {
            qiegejipanel.SetActive(true);
            interactivepanel.SetActive(false);
            playercontrol.enabled = false;
            ThirdPersonController.enabled = false;
        }
        if (this.CompareTag("diannao") && Input.GetKeyDown(KeyCode.F))
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

