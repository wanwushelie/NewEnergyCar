using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Camera c;
    private RaycastHit hitInfo;
    private HighlightableObject h;
    private string s;
    private bool isShow;
    private GameObject go;

    //字体
    public Font font;
    //观察目标
    public Transform Target;
    //观察距离
    public float Distance = 5F;
    //旋转速度
    private float SpeedX = 240;
    private float SpeedY = 120;
    //角度限制
    public float MinLimitY = 5;
    public float MaxLimitY = 180;

    //旋转角度
    private float mX = 0.0F;
    private float mY = 0.0F;

    //鼠标缩放距离最值
    public float MaxDistance = 10;
    public float MinDistance = 1.5F;
    //鼠标缩放速率
    private float ZoomSpeed = 2F;

    //是否启用差值
    public bool isNeedDamping = true;
    //速度
    public float Damping = 2.5F;

    void Start()
    {
        go = GameObject.Find("工件");
        c = GetComponent<Camera>();
        //初始化旋转角度
        mX = transform.eulerAngles.y;
        mY = transform.eulerAngles.x;
        //Debug.Log(mX);Debug.Log(mY);
    }

    void LateUpdate()
    {
        //点选物体实现高亮
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = c.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Three3"))
                {
                    if (hitInfo.collider.GetComponent<HighlightableObject>() == null)
                    {
                        hitInfo.collider.gameObject.AddComponent<HighlightableObject>();
                    }
                    h = hitInfo.collider.GetComponent<HighlightableObject>();
                    h.ConstantOn();
                    s = hitInfo.collider.gameObject.name;
                    isShow = true;

                }
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ne"))
                {
                    if (hitInfo.collider.GetComponent<HighlightableObject>() == null)
                    {
                        hitInfo.collider.gameObject.AddComponent<HighlightableObject>();
                    }
                    if (hitInfo.collider.gameObject.name == "yuanjian1" || hitInfo.collider.gameObject.name == "yuanjian2")
                    {
                        h = go.GetComponent<HighlightableObject>();
                    }
                    else
                        h = hitInfo.collider.GetComponent<HighlightableObject>();

                    h.ConstantOn();
                    s = hitInfo.collider.gameObject.name;
                    isShow = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (h != null)
                h.ConstantOff();
            s = "";
            isShow = false;
        }

        //鼠标右键旋转
        if (Target != null && Input.GetMouseButton(1))
        {
            //获取鼠标输入
            mX += Input.GetAxis("Mouse X") * SpeedX * 0.02F;
            mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02F;
            //范围限制
            mY = ClampAngle(mY, MinLimitY, MaxLimitY);
        }

        //鼠标滚轮缩放

        Distance -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);

        //重新计算位置和角度
        Quaternion mRotation = Quaternion.Euler(mY, mX, 0);

        Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + Target.position;

        //设置相机的角度和位置
        if (isNeedDamping)
        {
            //球形插值
            transform.rotation = Quaternion.Slerp(transform.rotation, mRotation, Time.deltaTime * Damping);
            //线性插值
            transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * Damping);
        }
        else
        {
            transform.rotation = mRotation;
            transform.position = mPosition;
        }

    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    private void OnGUI()
    {
        if (isShow)
        {
            if (s != "")
            {
                if (s == "yuanjian1" || s == "yuanjian2")
                {
                    GUIStyle style = new GUIStyle();
                    style.font = font;
                    style.normal.textColor = new Color32(255, 255, 255, 255);

                    GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 30, 30, 30), "工件", style);
                }
                else
                {
                    GUIStyle style = new GUIStyle();
                    style.font = font;
                    style.normal.textColor = new Color32(255, 255, 255, 255);

                    GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 30, 30, 30), s, style);
                }

            }

        }
    }
}

