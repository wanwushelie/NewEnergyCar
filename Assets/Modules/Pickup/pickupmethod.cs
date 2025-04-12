using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pickupmethod : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject hideobject;
    private GameObject hitObject;
    public GameObject panelpick, pu, pd, sh;
    public PickupController pickupController;
    public Text pickupText;
    public Button pickupButton;
    public Text objectNameText;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation = Quaternion.identity;
    public bool ischelun, isdizuo, istulun;
    public xiaochedate Xiaochedate;
    private ObjectData objectData;
    public ObjectDataManager objectdatamanager;
    public lingjianused lingjianused1;
    public Assemble assemble1;
    public Camera mainCamera;

    void Start()
    {
      
    }

    void Update()
    {
        if (Input.mousePosition != Vector3.zero)
            CheckForClickableObject();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CheckForClickableObject()
    {
        if (EventSystem.current.IsPointerOverGameObject() && !pickupController.IsHoldingObject)
            return;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)&& ObjectDataManager.Instance.GetData(hit.collider.gameObject.name)!=null)
        {
            hitObject = hit.collider.gameObject;
            pickupText.text = hitObject.name;
            Debug.Log(hitObject.name);

            if (IsPickupable(hitObject) && !pickupController.IsHoldingObject)
            {
                HandlePickupableObject();
            }
            else if (pickupController.IsHoldingObject && hit.collider.CompareTag("xiaoche"))
            {
                HandleHoldingObjectOnXiaoche();
            }
            else if (pickupController.IsHoldingObject && hitObject.name == "机械小车未完成" && ObjectDataManager.Instance.GetData(pickupController.heldObject.name).canBemakeup)
            {
                HandleAssembling();
            }
            else if (!pickupController.IsHoldingObject && hitObject.name == "机械小车未完成")
            {
                HandleDismantling();
            }
            else
            {
                ClearTargetAndHideUI();
            }
        }
        else
        {
            ClearTargetAndHideUI();
        }
    }

    bool IsPickupable(GameObject obj)
    {
        objectData = ObjectDataManager.Instance.GetData(obj.name);
        return objectData != null && objectData.canBePickedUp;
    }

    void HandlePickupableObject()
    {
        objectData = ObjectDataManager.Instance.GetData(hitObject.name);
        panelpick.SetActive(true);

        if (objectData.isshowed)
        {
            SetUI(false, false, true);
            CheckShowedInput();
        }
        else
        {
            StoreObjectPosition();
            SetUI(true, false, false);
            CheckPickupInput();
        }
    }
    void HandleHoldingObjectOnXiaoche()
    {
        panelpick.SetActive(true);
        pickupText.text = pickupController.heldObject.name;
        SetUI(false, true, false);
        CheckPutDownInput();
    }
    void HandleAssembling()
    {
        CheckAssembleInput();
        panelpick.SetActive(false);
    }

    void HandleDismantling()
    {
        CheckDismantleInput();
    }
    void StoreObjectPosition()
    {
        targetObject = hitObject;
        spawnPosition = hitObject.transform.position;
        spawnRotation = hitObject.transform.rotation;
    }
    void SetUI(bool pickupActive, bool putdownActive, bool useActive)
    {
        pu.SetActive(pickupActive);
        pd.SetActive(putdownActive);
        sh.SetActive(useActive);
    }

    void CheckShowedInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            lingjianused1.showed(hitObject);
        }
    }
    void CheckPickupInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnPickupX();
            assemble1.HideGameObjects.Add(hitObject);
        }
    }
    void CheckPutDownInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            pickupController.PutDown();
        }
    }

    void CheckAssembleInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            assemble1.assemble(pickupController.heldObject);
        }
    }

    void CheckDismantleInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            assemble1.chaixieUI.SetActive(true);
        }
    }

    public void OnPickupX()
    {
        if (targetObject != null)
        {
            pickupController.Pickup(targetObject);
            ClearTargetAndHideUI();
        }
    }
    void ClearTargetAndHideUI()
    {
        targetObject = null;
        panelpick.SetActive(false);
        pickupText.text = "";
        SetUI(false, false, false);
    }
}