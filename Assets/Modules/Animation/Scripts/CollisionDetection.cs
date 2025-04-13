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
    //public FirstPersonCameraController player;
    public PlayableDirector director;
    public List<TimelineAsset> timelines;
    public GameObject tipsUI;
    public GameObject skipPrompt; // 跳过提示的UI
    public int currentcount;
    public bool isTure = false;
    private bool isPlaying = false;
    public int currentTaskIndex = 0;// 默认动画为0，发布任务0，（0完成）正式交互时第一个动画应该为1，发布任务1
    public CharacterMovement characterMovement;
    public bool iscom1 = false;
    public GameObject aistart;
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

        currentcount = timelines.Count;
        if (director.state == PlayState.Playing)
        {
            skipPrompt.SetActive(true);
            tipsUI.SetActive(false);
            characterMovement.canMove = false;
            // 按下X键跳过当前剧情
            if (Input.GetKeyDown(KeyCode.X))
            {
                SkipTimeline();
                skipPrompt.SetActive(false);
                Debug.Log("X键被按下");
            }
        }
        else
        {
            skipPrompt.SetActive(false);
            characterMovement.canMove =true;
            TaskManager taskManager = TaskManager.Instance;
            if(taskManager.taskDisplay.firstIncompleteTask == taskManager.tasks[1]&&!iscom1)
            {
                TaskManager.Instance.UpdateTaskUI();
                iscom1 = true;
            }
            else if (taskManager.taskDisplay.firstIncompleteTask == taskManager.tasks[0])
            {
                StartCoroutine(waitfornextanimation());
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
        TaskManager taskManager = TaskManager.Instance;
        if (isTure && !isPlaying&&taskManager.taskDisplay.firstIncompleteTask.isCompleted)
        {
            //iscomplete = taskManager.taskDisplay.firstIncompleteTask.isCompleted;
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
            //player.canMove = false;
            
            PlayTimeline(currentTaskIndex+1);
            while (director.state == PlayState.Playing)
            {
                yield return null;
            }

            //player.canMove = true;
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

    public IEnumerator waitfornextanimation()
    {
        TaskManager taskManager = TaskManager.Instance;
        yield return new WaitForSeconds(1.5f);
        taskManager.taskDisplay.firstIncompleteTask = taskManager.tasks[1];
        currentTaskIndex = 1;
        aistart.SetActive(true);
        PlayTimeline(1);
    }

    public void PlayTimeline(int index)
    {
        if (index <timelines.Count)
        {
            Debug.Log("播放");
            TimelineAsset currentTimeline = timelines[index];
            director.playableAsset = currentTimeline;
            if (index!=0)
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