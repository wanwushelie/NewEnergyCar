using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class NPCManager : MonoBehaviour
{
    public CharacterLocator characterLocator;
    public TimelinePlayer timelinePlayer;
    public TimelineManager timelineManager;

    // 当触发碰撞时播放 Timeline
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("npc"))
        {
            PlayNPCTimeline(other.gameObject);
        }
    }

    private void PlayNPCTimeline(GameObject npc)
    {
        // 获取 NPC 的 Timeline 标识符（假设每个 NPC 有一个唯一标识符）
        string timelineKey = npc.name; // 或其他唯一标识符
        PlayableDirector director = timelineManager.GetTimeline(timelineKey);

        if (director != null)
        {
            // 绑定并播放 Timeline
            timelinePlayer.BindTimeline(director);
            StartCoroutine(MoveAndPlayTimeline(director));
        }
    }

    private IEnumerator MoveAndPlayTimeline(PlayableDirector director)
    {
        // 获取目标位置
        Transform targetPos = director.transform.Find("Target1");
        if (targetPos != null)
        {
            // 启用虚拟相机
            director.transform.Find("VC1").gameObject.SetActive(true);

            // 移动到目标位置
            yield return characterLocator.StartCoroutine(
                characterLocator.SmoothMove(targetPos, 0.5f)
            );

            // 播放 Timeline
            timelinePlayer.PlayTimeline();

            // 等待动画播放完成
            while (director.state == PlayState.Playing)
            {
                yield return null;
            }
        }
    }
}