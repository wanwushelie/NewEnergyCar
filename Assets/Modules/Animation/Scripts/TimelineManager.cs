using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{ 
    // 存储所有可触发的 Timeline
    private Dictionary<string, PlayableDirector> timelines = new Dictionary<string, PlayableDirector>();

    // 添加 Timeline
    public void AddTimeline(string key, PlayableDirector director)
    {
        if (!timelines.ContainsKey(key))
        {
            timelines.Add(key, director);
        }
    }

    // 获取 Timeline
    public PlayableDirector GetTimeline(string key)
    {
        timelines.TryGetValue(key, out PlayableDirector director);
        return director;
    }

    // 删除 Timeline
    public void RemoveTimeline(string key)
    {
        if (timelines.ContainsKey(key))
        {
            timelines.Remove(key);
        }
    }

    // 播放 Timeline
    public bool PlayTimeline(string key)
    {
        if (timelines.TryGetValue(key, out PlayableDirector director))
        {
            director.Play();
            return true;
        }
        return false;
    }

    // 停止所有 Timeline
    public void StopAllTimelines()
    {
        foreach (var director in timelines.Values)
        {
            if (director.state == PlayState.Playing)
            {
                director.Stop();
            }
        }
    }
}