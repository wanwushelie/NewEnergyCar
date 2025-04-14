using System.Collections;
using System.Net.NetworkInformation;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
//using UnityEditorInternal;
using UnityEngine;


public class lingjianused : MonoBehaviour
{
    public GameObject youhusposition, youhueposition,youhuoposition;
    public GameObject banshousposition, banshoueposition, banshouoposition,kpp1s,kpp1e,kpp2s,kpp2e,kpp3e,kpp3s;
    public GameObject dingzi,dingzioposition,dingzieposition,dianji,banshou;//�����ƶ�
    public GameObject zhuanzhou, kapan,kapanpart1,kapanpart2,kapanpart3;//������ת
    public GameObject DaoJu1, DaoJu2, DaoJu3;
    public GameObject DaoJu1oposition, DaoJu2oposition, DaoJu3oposition, DaoJuposition;//����λ��
    public GameObject yuanjian, yuanjian0position, yuanjian1position, yuanjian2position,yuanjian3position;
    public GameObject bloodpanel,currentobj,lizi;
    public playmusic playmusic1;
    public float speed = 1.0f;
    public float duration = 2.0f;
    public pickupmethod pickupmethod1;
    public bool isdianyuan=false,isdianji=false,isrotate=false,isdaojia=false,isdaoju=false,iskapan=false,isyuanjian=false,isbanshou=false;
    private Renderer renderer; // 物体的 Renderer 组件
    private Color originalColor; // 保存原始颜色
    private StateMachine stateMachine;
    public RefinedColorTransition refinedColorTransition;
    public bool haveshowed = false;

    public void showed(GameObject obj)
    {
        currentobj = obj;
        haveshowed = true;
        switch (obj.name)
        {
            case "油壶":
                useYouhu(obj);
                break;
            case "电源开关":
                usedianyuan(obj);
                break;
            case "电机开关":
                usedianji(obj);
                break;
            case "扳手":
                usebanshou(obj);
                break;
            case "刀架开关":
                usedaojia(obj);
                break;
            case "外圆车刀":
                usewaiyaun(obj);
                break;
            case "切槽刀":
                useqiecao(obj);
                break;
            case "螺纹刀":
                useluowen(obj);
                break;
            case "元件":
                useyuanjian(obj);
                break;
        }

        pickupmethod1.panelpick.SetActive(false);
    }
    IEnumerator MoveAndReturnYouHu(GameObject targetObj,Transform ori, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        yield return MoveToPosition(targetObj, ori.position, startPosition, duration);
        yield return new WaitForSeconds(1.0f);
        yield return MoveToPosition(targetObj, startPosition, endPosition, duration);
        yield return new WaitForSeconds(1.0f);
        targetObj.transform.rotation = new Quaternion(0, 0, 0,0);
        yield return MoveToPosition(targetObj, endPosition, ori.position, duration);
        
    }
    IEnumerator ReturnBanShou(GameObject targetObj, Transform ori, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        // ��һ�Σ�����㵽�յ�
        yield return MoveToPosition(targetObj, ori.position, startPosition, duration);
        yield return new WaitForSeconds(1.0f);
        // �ڶ��Σ����յ㷵�����
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
        targetObj.transform.position = end; 
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3.0f);
        lizi.SetActive(false);
    }
    IEnumerator MoveRotation(GameObject obj,Quaternion start,Quaternion end,float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // ��ֵ����
            obj.transform.rotation = Quaternion.Slerp(start, end, t); // ʹ�� Slerp ƽ����ֵ
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.rotation = end;
    }
    IEnumerator RotateContinuously(GameObject obj)
    {
        while (isrotate)
        { 
            obj.transform.Rotate(Vector3.right * 180.0f * Time.deltaTime);
            yield return null; // �ȴ���һ֡
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
    public void putback()
    {
        isdianji = false;
        isrotate = false;
        StartCoroutine(MoveRotation(currentobj, Quaternion.Euler(-110, 0, 0), Quaternion.Euler(-90, 0, 0), 1.0f));
        StopCoroutine(RotateContinuously(kapan));
        StopCoroutine(RotateContinuously(zhuanzhou));
        playmusic1.Pause();
        bloodpanel.SetActive(false);
    }
    public void error()
    {
        //bloodpanel.SetActive(true);
        playmusic1.Playjinggao();
        //lizi.SetActive(true);
        refinedColorTransition.StartFullTransition();
        StartCoroutine(wait());
    }
    public void useYouhu(GameObject obj)
    {
        obj.transform.rotation = Quaternion.Euler(0, -90, 90);
        StartCoroutine(MoveAndReturnYouHu(obj, youhuoposition.transform, youhusposition.transform.position, youhueposition.transform.position, duration));
    }
    public void usedianyuan(GameObject obj)
    {
        renderer = obj.GetComponent<Renderer>();
        originalColor = renderer.material.color;
        if (!isdianyuan)
        {
            renderer.material.color = Color.green;
            isdianyuan = true;
            obj.transform.rotation = Quaternion.Euler(-110, -90, 90);
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
    }
    public void usedianji(GameObject obj)
    {
        if (!isdianji)
        {
            isdianji = true;
            StartCoroutine(MoveRotation(obj, Quaternion.Euler(-90, 0, 0), Quaternion.Euler(-110, 0, 0), 1.0f));
            if (isdianyuan)
            {
                isrotate = true;
                StartCoroutine(RotateContinuously(kapan));
                StartCoroutine(RotateContinuously(zhuanzhou));
                playmusic1.Playxuanzhuan();
                if (isbanshou)
                {
                    error();
                }
            }
        }
        else
        {
            isdianji = false;
            isrotate = false;
            StartCoroutine(MoveRotation(obj, Quaternion.Euler(-110, 0, 0), Quaternion.Euler(-90, 0, 0), 1.0f));
            StopCoroutine(RotateContinuously(kapan));
            StopCoroutine(RotateContinuously(zhuanzhou));
            playmusic1.Pause();
        }
    }
    public void usebanshou(GameObject obj)
    {
        if (!iskapan&&!isbanshou)
        {
            if (isdianji&&isdianyuan)
            {
                error();
            }
            else
            {
                StartCoroutine(MoveBanShou(obj, banshouoposition.transform, Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 90, 0), banshousposition.transform.position, banshoueposition.transform.position, duration));
                iskapan = true;
                isbanshou = true;
            }
        }
        else if(iskapan&&isbanshou)
        {
            StartCoroutine(ReturnBanShou(obj, banshoueposition.transform, banshousposition.transform.position, banshouoposition.transform.position, duration));
            isbanshou = false;
        }
    }
    public void usedaojia(GameObject obj)
    {
        if (!isdaojia)
        {
            StartCoroutine(MoveRotation(obj, Quaternion.Euler(-90, 0, 0), Quaternion.Euler(-90, 0, -15), 1.0f));
            StartCoroutine(MoveToPosition(dingzi, dingzioposition.transform.position, dingzieposition.transform.position, 1.0f));
            isdaojia = true;
        }
        else if (isdaojia && !isdaoju)
        {
            StartCoroutine(MoveRotation(obj, Quaternion.Euler(-90, 0, -15), Quaternion.Euler(-90, 0, 0), 1.0f));
            StartCoroutine(MoveToPosition(dingzi, dingzieposition.transform.position, dingzioposition.transform.position, 1.0f));
            isdaojia = false;
        }
    }
    public void usewaiyaun(GameObject obj)
    {
        if (isdaojia && !isdaoju)
        {
            StartCoroutine(MoveToPosition(obj, DaoJu1oposition.transform.position, DaoJuposition.transform.position, 2.0f));
            isdaoju = true;
        }
        else if (isdaojia && isdaoju)
        {
            StartCoroutine(MoveToPosition(obj, DaoJuposition.transform.position, DaoJu1oposition.transform.position, 2.0f));
            isdaoju = false;
        }
    }
    public void useqiecao(GameObject obj)
    {
        if (isdaojia && !isdaoju)
        {
            StartCoroutine(MoveToPosition(obj, DaoJu2oposition.transform.position, DaoJuposition.transform.position, 2.0f));
            isdaoju = true;
        }
        else if (isdaojia && isdaoju)
        {
            StartCoroutine(MoveToPosition(obj, DaoJuposition.transform.position, DaoJu2oposition.transform.position, 2.0f));
            isdaoju = false;
        }
    }
    public void useluowen(GameObject obj)
    {
        if (isdaojia && !isdaoju)
        {
            StartCoroutine(MoveToPosition(obj, DaoJu3oposition.transform.position, DaoJuposition.transform.position, 2.0f));
            isdaoju = true;
        }
        else if (isdaojia && isdaoju)
        {
            StartCoroutine(MoveToPosition(obj, DaoJuposition.transform.position, DaoJu3oposition.transform.position, 2.0f));
            isdaoju = false;
        }
    }
    public void useyuanjian(GameObject obj)
    {
        if (iskapan && !isyuanjian)
        {
            StartCoroutine(MoveAndReturnYuanJian(obj, yuanjian0position.transform.position, yuanjian1position.transform.position, yuanjian2position.transform.position, yuanjian3position.transform.position, 2.0f));
            isyuanjian = true;
            //playmusic1.Playqiege();
            if(isdianji&&isdianyuan)
            {
                error();
            }
        }
        else if (iskapan && isyuanjian)
        {
            StartCoroutine(MoveAndReturnYuanJian(obj, yuanjian3position.transform.position, yuanjian2position.transform.position, yuanjian1position.transform.position, yuanjian0position.transform.position, 2.0f));
            isyuanjian = false;
            playmusic1.Pause();
        }
    }
}