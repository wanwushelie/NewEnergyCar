using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using StarterAssets;
using UnityEngine.Timeline; // 添加这一行来引用TimelineAsset类型


public class CollisionDetection : MonoBehaviour
{
    public PlayableDirector director; // 绑定到场景中的PlayableDirector组件
    public List<TimelineAsset> timelines; // 存储所有任务相关的 Timeline 资源

    // 公开的游戏物体变量
    public GameObject indicatorObject;

    private bool isPlaying =  false;
    private bool isTure = false;//是否按下F后运行播放动画
    //按键F操作
    IEnumerator OnOprerate() 
    {
        Debug.Log("OnOperate");
        if(isTure && !isPlaying) {
            isPlaying = true;
            var thirdPersonController = GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = false;
            
            PlayTimeline(0);
            while (director.state == PlayState.Playing) {
                yield return null;
            }
            thirdPersonController.enabled = true;
            // PlayerFollowCamera.SetActive(true);  
            isPlaying = false;
        }
    }
    
    public void PlayTimeline(int index)
    {
        if (index < timelines.Count)
        {
            TimelineAsset currentTimeline = timelines[index];
            director.playableAsset = currentTimeline;

            // 绑定输出轨道（如果有需要）
            // foreach (var output in director.playableAsset.outputs)
            // {
            //     if (output.streamName == "Player Track")
            //     {
            //         var animator = GetComponent<Animator>();
            //         director.SetGenericBinding(output.sourceObject, animator);
            //         break;
            //     }
            // }
            // 隐藏公开的游戏物体变量
            if (indicatorObject != null)
            {
                indicatorObject.SetActive(false);
            }

            director.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "npc")
        {
            Debug.Log("OnTriggerEnter");
            isTure = true;

            // 激活公开的游戏物体变量
            if (indicatorObject != null)
            {
                indicatorObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "npc")
        {
            Debug.Log("OnTriggerExit");
            isTure = false;

            // 隐藏公开的游戏物体变量
            if (indicatorObject != null)
            {
                indicatorObject.SetActive(false);
            }
        }
    }
}