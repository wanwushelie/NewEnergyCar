using System.Collections;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class lingjianused : MonoBehaviour
{
    public GameObject youhusposition, youhueposition,youhuoposition;//油壶移动
    public GameObject banshousposition, banshoueposition, banshouoposition,kpp1s,kpp1e,kpp2s,kpp2e,kpp3e,kpp3s;//扳手移动,卡盘位置移动
    public GameObject dingzi,dingzioposition,dingzieposition;//钉子移动
    public GameObject zhuanzhou, kapan,kapanpart1,kapanpart2,kapanpart3;//卡盘旋转
    public GameObject DaoJu1, DaoJu2, DaoJu3;
    public GameObject DaoJu1oposition, DaoJu2oposition, DaoJu3oposition, DaoJuposition;//刀具位置
    public GameObject yuanjian, yuanjian0position, yuanjian1position, yuanjian2position,yuanjian3position;//元件位置
    public float speed = 1.0f;
    public float duration = 2.0f;
    public pickupmethod pickupmethod1;
    public bool isdianyuan=false,isdianji=false,isrotate=false,isdaojia=false,isdaoju=false,iskapan=false,isyuanjian=false;//检测电源和电机是否使用
    private Renderer renderer; // 物体的 Renderer 组件
    private Color originalColor; // 保存原始颜色

    void Start()
    {
        if (youhusposition == null || youhueposition == null)
        {
            Debug.LogError("YouHu start or end position is not set!");
        }
    }

    public void showed(GameObject obj)
    {
        switch (obj.name)
        {
            case "YouHu":
                //obj.transform.position = youhusposition.transform.position;
                obj.transform.rotation = Quaternion.Euler(0, -90, 90);
                StartCoroutine(MoveAndReturnYouHu(obj, youhuoposition.transform, youhusposition.transform.position, youhueposition.transform.position, duration));
                break;
            case "电源开关":
                renderer = obj.GetComponent<Renderer>();
                originalColor = renderer.material.color;
               //Debug.Log("95959");
                if(!isdianyuan)
                {
                    renderer.material.color = Color.green;
                    isdianyuan = true;
                    obj.transform.rotation = Quaternion.Euler(-110,-90,90);
                    if (isdianji)
                    {
                        isrotate = true;
                        StartCoroutine(RotateContinuously(kapan));
                        StartCoroutine(RotateContinuously(zhuanzhou));
                    }
                }
                else
                {
                    renderer.material.color = originalColor;
                    isdianyuan = false;
                    isrotate = false;
                    obj.transform.rotation = Quaternion.Euler(-90, -90, 90);
                    StopCoroutine(RotateContinuously(kapan));
                    StopCoroutine(RotateContinuously(zhuanzhou));
                }
                break;
            case "电机开关":
                if (!isdianji)
                {
                    isdianji = true;
                    StartCoroutine(MoveRotation(obj, Quaternion.Euler(-90, 0, 0), Quaternion.Euler(-110, 0, 0), 1.0f));
                    if(isdianyuan)
                    {
                        isrotate = true;
                        StartCoroutine(RotateContinuously(kapan));
                        StartCoroutine(RotateContinuously(zhuanzhou));
                    }
                }
                else
                {
                    isdianji = false;
                    isrotate = false;
                    StartCoroutine(MoveRotation(obj, Quaternion.Euler(-110, 0, 0), Quaternion.Euler(-90, 0, 0), 1.0f));
                    StopCoroutine(RotateContinuously(kapan));
                    StopCoroutine(RotateContinuously(zhuanzhou));
                }
                break;
            case "扳手":
                if (!iskapan)
                {
                    StartCoroutine(MoveBanShou(obj, banshouoposition.transform, Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 90, 0), banshousposition.transform.position, banshoueposition.transform.position, duration));
                    iskapan = true;
                }
                else
                {
                    StartCoroutine(ReturnBanShou(obj, banshoueposition.transform, banshousposition.transform.position, banshouoposition.transform.position, duration));
                    iskapan = false;
                }
                break;
            case "刀架开关":
                if (!isdaojia)
                {
                    StartCoroutine(MoveRotation(obj, Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -16), 1.0f));
                    StartCoroutine(MoveToPosition(dingzi, dingzioposition.transform.position,dingzieposition.transform.position, 1.0f));
                    isdaojia = true;
                }
                else
                {
                    StartCoroutine(MoveRotation(obj, Quaternion.Euler(0, 0, -16), Quaternion.Euler(0, 0, 0), 1.0f));
                    StartCoroutine(MoveToPosition(dingzi, dingzieposition.transform.position, dingzioposition.transform.position, 1.0f));
                    isdaojia = false;
                }
                break;
            case "DaoJu1":
                if(isdaojia&&!isdaoju)
                { 
                    StartCoroutine(MoveToPosition(obj, DaoJu1oposition.transform.position, DaoJuposition.transform.position, 2.0f));
                    isdaoju = true;
                }
                break;
            case "DaoJu2":
                if (isdaojia && !isdaoju)
                {
                    StartCoroutine(MoveToPosition(obj, DaoJu2oposition.transform.position, DaoJuposition.transform.position, 2.0f));
                    isdaoju = true;
                }
                break;
            case "DaoJu3":
                if (isdaojia && !isdaoju)
                {
                    StartCoroutine(MoveToPosition(obj, DaoJu3oposition.transform.position, DaoJuposition.transform.position, 2.0f));
                    isdaoju = true;
                }
                break;
            case "元件":
                if(iskapan&&!isyuanjian)
                {
                    StartCoroutine(MoveAndReturnYuanJian(obj,yuanjian0position.transform.position,yuanjian1position.transform.position,yuanjian2position.transform.position,yuanjian3position.transform.position,2.0f));
                    isyuanjian = true;
                }
                else if(iskapan&&isyuanjian)
                {
                    StartCoroutine(MoveAndReturnYuanJian(obj, yuanjian3position.transform.position, yuanjian2position.transform.position, yuanjian1position.transform.position, yuanjian0position.transform.position, 2.0f));
                    isyuanjian = false;
                }
                break;
        }

        pickupmethod1.panelpick.SetActive(false);
    }
    IEnumerator MoveAndReturnYouHu(GameObject targetObj,Transform ori, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        // 第一段：从起点到终点
        yield return MoveToPosition(targetObj, ori.position, startPosition, duration);
        yield return new WaitForSeconds(1.0f);
        // 第二段：从终点返回起点
        yield return MoveToPosition(targetObj, startPosition, endPosition, duration);
        yield return new WaitForSeconds(1.0f);
        targetObj.transform.rotation = new Quaternion(0, 0, 0,0);
        yield return MoveToPosition(targetObj, endPosition, ori.position, duration);
        
    }
    IEnumerator ReturnBanShou(GameObject targetObj, Transform ori, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        // 第一段：从起点到终点
        yield return MoveToPosition(targetObj, ori.position, startPosition, duration);
        yield return new WaitForSeconds(1.0f);
        // 第二段：从终点返回起点
        yield return MoveToPosition(targetObj, startPosition, endPosition, duration);
        

    }
    IEnumerator MoveBanShou(GameObject targetObj, Transform ori, Quaternion start, Quaternion end,Vector3 startPosition, Vector3 endPosition, float duration)
    {
        yield return MoveToPosition(targetObj, ori.position, startPosition, duration);
        yield return new WaitForSeconds(0.5f);
        yield return MoveToPosition(targetObj, startPosition, endPosition, duration);
        yield return new WaitForSeconds(0.5f);
        MoveRotation( targetObj, start, end, 2.0f);
        StartCoroutine(MoveToPosition(kapanpart1, kpp1s.transform.position, kpp1e.transform.position, duration));
        StartCoroutine(MoveToPosition(kapanpart2, kpp2s.transform.position, kpp2e.transform.position, duration));
        StartCoroutine(MoveToPosition(kapanpart3, kpp3s.transform.position, kpp3e.transform.position, duration));
    }
    IEnumerator MoveToPosition(GameObject targetObj, Vector3 start, Vector3 end, float duration)
    {
       
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            targetObj.transform.position = Vector3.Lerp(start, end, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        targetObj.transform.position = end; // 确保最终到达终点
        //yield return new WaitForSeconds(1.0f);
       
    }
    IEnumerator MoveRotation(GameObject obj,Quaternion start,Quaternion end,float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // 插值参数
            obj.transform.rotation = Quaternion.Slerp(start, end, t); // 使用 Slerp 平滑插值
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终旋转到目标位置
        obj.transform.rotation = end;
    }
    IEnumerator RotateContinuously(GameObject obj)
    {
        while (isrotate)
        {
            // 每帧绕 X 轴旋转
            obj.transform.Rotate(Vector3.right * 180.0f * Time.deltaTime);
            yield return null; // 等待下一帧
        }
    }
    IEnumerator MoveAndReturnYuanJian(GameObject targetObj, Vector3 position0, Vector3 position1, Vector3 position2,Vector3 position3, float duration)
    {
        yield return MoveToPosition(targetObj,position0, position1, duration);
        yield return new WaitForSeconds(1.0f);
        yield return MoveToPosition(targetObj, position1, position2, duration);
        yield return new WaitForSeconds(1.0f);
        targetObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        yield return MoveToPosition(targetObj, position2, position3, duration);

    }
}