using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    public Animator animator;
    private PlayableDirector director;

    public void BindTimeline(PlayableDirector director)
    {
        this.director = director;

        foreach (var output in director.playableAsset.outputs)
        {
            if (output.streamName == "Player Track")
            {
                director.SetGenericBinding(output.sourceObject, animator);
                break;
            }
        }
    }

    public void PlayTimeline()
    {
        if (director != null)
        {
            director.Play();
        }
    }

    public void StopTimeline()
    {
        if (director != null)
        {
            director.Stop();
        }
    }
}