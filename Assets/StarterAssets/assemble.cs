using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class assemble : MonoBehaviour
{
    public PickupController pickupController;
    public pickupmethod pickupmethod;
    public GameObject dizuo,chelun,tulun;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void assemblecar()
    {
        //if (EventSystem.current.IsPointerOverGameObject() && !pickupController.IsHoldingObject) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if(gameObject.name=="机械小车未完成"&& pickupController.IsHoldingObject)
            {
                Debug.Log("已识别");
                if (Input.GetMouseButtonDown(0))
                {
                    switch(pickupmethod.pickupname)
                    {
                        case "tulun":
                            tulun.SetActive(true);
                            break;
                        case "chelun":
                            chelun.SetActive(true);
                            break;
                        case "dizuo":
                            dizuo.SetActive(true);
                            break;
                    }
                }
                }
        }
    }
}
