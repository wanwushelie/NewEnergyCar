using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;


public class Assemble : MonoBehaviour
{
    public pickupmethod pickupmethod1;
    public lingjiandate lingjiandate1;
    public GameObject dizuo, chelun, tulun1, tulun2;
    public GameObject chaixieUI, chelunpart, tulunpart, dizuopart;//œ‘ æui
    public xiaochedate xiaochedate1;
    public PickupController pickupController;
    public List<GameObject> HideGameObjects = new List<GameObject>();
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void assemble(GameObject car)
    {

        car.transform.parent = null;
        car.transform.position = pickupmethod1.spawnPosition;
        car.transform.rotation = pickupmethod1.spawnRotation;
            switch (car.name)
            {
                case "Ω ÙÕπ¬÷":
                    if (!pickupmethod1.istulun)
                    {
                        tulun1.SetActive(true);
                        tulun2.SetActive(true);
                        tulunpart.SetActive(true);
                        xiaochedate1.totalweight += lingjiandate1.C[3].weight;
                        xiaochedate1.totalwending += lingjiandate1.C[3].wending;
                        pickupmethod1.istulun = true;
                        car.SetActive(false);
                        pickupController.heldObject = null;
                    }
                    break;
                case "À‹¡œÕπ¬÷":
                    if (!pickupmethod1.istulun)
                    {
                        tulun1.SetActive(true);
                        tulun2.SetActive(true);
                        tulunpart.SetActive(true);
                        xiaochedate1.totalweight += lingjiandate1.C[2].weight;
                        xiaochedate1.totalwending += lingjiandate1.C[2].wending;
                        pickupController.heldObject = null;
                        pickupmethod1.istulun = true;
                        car.SetActive(false);
                    }
                    break;
                case "Ω Ù≥µ¬÷":
                    if (!pickupmethod1.ischelun)
                    {

                        car.transform.rotation = pickupmethod1.spawnRotation;
                        chelun.SetActive(true);
                        pickupmethod1.ischelun = true;
                        chelunpart.SetActive(true);
                        xiaochedate1.totalweight += lingjiandate1.C[1].weight;
                        xiaochedate1.totalwending += lingjiandate1.C[1].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
                case "À‹¡œ≥µ¬÷":
                    if (!pickupmethod1.ischelun)
                    {

                        chelun.SetActive(true);
                        pickupmethod1.ischelun = true;
                        chelunpart.SetActive(true);
                        xiaochedate1.totalweight += lingjiandate1.C[0].weight;
                        xiaochedate1.totalwending += lingjiandate1.C[0].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
                case "Ω Ùµ◊◊˘":
                    if (!pickupmethod1.isdizuo)
                    {

                        dizuo.SetActive(true);
                        pickupmethod1.isdizuo = true;
                        dizuopart.SetActive(true);
                        xiaochedate1.totalweight += lingjiandate1.C[5].weight;
                        xiaochedate1.totalwending += lingjiandate1.C[5].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
                case "À‹¡œµ◊◊˘":
                    if (!pickupmethod1.isdizuo)
                    { 
                        dizuo.SetActive(true);
                        pickupmethod1.isdizuo = true;
                        dizuopart.SetActive(true);
                        xiaochedate1.totalweight += lingjiandate1.C[4].weight;
                        xiaochedate1.totalwending += lingjiandate1.C[4].wending;
                        pickupController.heldObject = null;
                        car.SetActive(false);
                    }
                    break;
            pickupController.heldObject = null;
        }
    }
    public void Disassemblydizuo()
    {
        if (pickupmethod1.isdizuo)
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("dizuo"));
            foundObject.SetActive(true);
            lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                if (foundObject.name == lingjiandate.C[i].gameobjectname)
                {
                    xiaochedate1.totalweight -= lingjiandate.C[i].weight;
                    xiaochedate1.totalwending -= lingjiandate.C[i].wending;
                    break;
                }
            }
            dizuo.SetActive(false);
           HideGameObjects.Remove(foundObject);
        }
        else
        {

        }
        pickupmethod1.isdizuo = false;
    }
    public void Disassemblychelun()
    {
        if (pickupmethod1.ischelun)
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("chelun"));
            lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();
            foundObject.SetActive(true);
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                if (foundObject.name == lingjiandate.C[i].gameobjectname)
                {
                    xiaochedate1.totalweight -= lingjiandate.C[i].weight;
                    xiaochedate1.totalwending -= lingjiandate.C[i].wending;
                    break;
                }
            }
            chelun.SetActive(false);
            HideGameObjects.Remove(foundObject);
        }
        else
        { }
        pickupmethod1.ischelun = false;
    }
    public void Disassemblytulun()
    {
        if (pickupmethod1.istulun)
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("tulun"));
            if (foundObject != null)
            {
                foundObject.SetActive(true);
            }
            else
                Debug.LogError("Œ¥’“µΩ∆•≈‰µƒ∂‘œÛ£°");
            lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                if (foundObject.name == lingjiandate.C[i].gameobjectname)
                {
                    xiaochedate1.totalweight -= lingjiandate.C[i].weight;
                    xiaochedate1.totalwending -= lingjiandate.C[i].wending;
                    break;
                }
            }
            tulun1.SetActive(false);
            tulun2.SetActive(false);
            HideGameObjects.Remove(foundObject);
        }
        else
        { }
        pickupmethod1.istulun = false;
    }
}


