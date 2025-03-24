using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using StarterAssets;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Cinemachine;

public class CollisionDetection : MonoBehaviour
{
    public FirstPersonCameraController player;
    public PlayableDirector director;
    public List<TimelineAsset> timelines;
    public GameObject tipsUI;
    public GameObject skipPrompt; // 跳过提示的UI

    private bool isPlaying = false;
    private bool isTure = false;//是否在可交互范围内
    private int currentTaskIndex = 0;// 默认动画为0，发布任务0，（0完成）正式交互时第一个动画应该为1，发布任务1

    private void Start()
    {
        isPlaying = true;
        //打印isPlaying
        Debug.Log("isPlaying:" + isPlaying);
        // player.canMove = false;
        //播放第一段动画
        PlayTimeline(0);
        isPlaying = false;
    }
    private void Update()
    {
        //如果动画正在播放
        if (director.state == PlayState.Playing)
        {
            skipPrompt.SetActive(true);
            // 按下X键跳过当前剧情
            if (Input.GetKeyDown(KeyCode.X))
            {
                SkipTimeline();
                skipPrompt.SetActive(false);
                // player.canMove = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F键被按下");
            StartCoroutine(OnOperate());
        }
       
    }
    public IEnumerator OnOperate()
    {
        Debug.Log("OnOperate");

        if (isTure && !isPlaying)
        {
            
            TaskManager taskManager = TaskManager.Instance;
            if (taskManager != null)
            {
                Task currentTask = taskManager.tasks.Find(task => task.id == (currentTaskIndex).ToString());
                if (currentTask != null && !currentTask.isCompleted)
                {
                    Debug.Log("任务未完成");
                    yield break;
                }
            }

            isPlaying = true;
            player.canMove = false;
            //
            PlayTimeline(currentTaskIndex+1);
            while (director.state == PlayState.Playing)
            {
                yield return null;
            }

            player.canMove = true;
            //更新任务UpdateTaskUI
            TaskManager.Instance.UpdateTaskUI();
            currentTaskIndex = Mathf.Min(currentTaskIndex + 1, timelines.Count - 1);

            isPlaying = false;

            // 隐藏跳过提示
            if (skipPrompt != null)
            {
                skipPrompt.gameObject.SetActive(false);
            }
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
            //  隐藏任务系统 UI
            TaskManager.Instance.HideTaskSystemUI();
            // 隐藏UI
            if (tipsUI != null)
            {
                tipsUI.SetActive(false);
            }

            // 显示跳过提示
            if (skipPrompt != null)
            {
                skipPrompt.gameObject.SetActive(true);
            }

            director.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //是npc且动画没在播放
        if (other.gameObject.tag == "npc" && !isPlaying)
        {
            Debug.Log("OnTriggerEnter");
            isTure = true;

            // 激活提示UI
            if (tipsUI != null)
            {
                tipsUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "npc" && !isPlaying)
        {
            Debug.Log("OnTriggerExit");
            isTure = false;

            // 隐藏提示UI
            if (tipsUI != null)
            {
                tipsUI.SetActive(false);
            }
        }
    }

    // 跳过当前剧情的逻辑
    public void SkipTimeline()
    {
        Debug.Log("跳过当前剧情");

        // 获取当前 Timeline 的总时长
        float totalDuration = (float)director.playableAsset.duration;

        // 将播放时间设置为总时长，让动画停留在最后一帧
        director.time = totalDuration;
        director.Evaluate();
        // isPlaying = false;
    }
}